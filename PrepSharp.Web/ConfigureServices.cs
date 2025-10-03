using Hangfire;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using PrepSharp.Consts;
using PrepSharp.Web.Core;
using PrepSharp.Web.Helpers;
using PrepSharp.Web.Services;
using System.Reflection;
using System.Security.Claims;
using UoN.ExpressiveAnnotations.NetCore.DependencyInjection;

namespace PrepSharp.Web;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        // Retrieve the database connection string from configuration
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        // Register the application's DbContext with SQL Server support
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                connectionString,
                sqlOptions => sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName) // Specify the assembly containing the migrations
            ));

        // Add a developer exception page for database-related errors
        services.AddDatabaseDeveloperPageExceptionFilter();

        // Add Identity services
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            // add the email token provider to the identity options (this is used to send the email confirmation token to the user)
            options.Tokens.ProviderMap.Add("Email", new TokenProviderDescriptor(typeof(EmailTokenProvider<ApplicationUser>)));
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultUI()
        .AddDefaultTokenProviders()
        .AddSignInManager<SignInManager<ApplicationUser>>();

        // Add External Authentication Providers
        // Bind GoogleAuthSettings
        var googleAuthSettings = builder.Configuration
            .GetSection("Authentication:Google")
            .Get<GoogleAuthSettings>() ?? throw new InvalidOperationException("Google authentication settings are missing.");

        // Bind FacebookAuthSettings
        var facebookAuthSettings = builder.Configuration
            .GetSection("Authentication:Facebook")
            .Get<FacebookAuthSettings>() ?? throw new InvalidOperationException("Facebook authentication settings are missing.");

        services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = googleAuthSettings.ClientId;
                options.ClientSecret = googleAuthSettings.ClientSecret;

                options.Events.OnRemoteFailure = context =>
                {
                    context.HandleResponse();
                    context.Response.Redirect("/Identity/Account/Login");
                    return Task.CompletedTask;
                };
            })
            .AddFacebook(options =>
            {
                options.AppId = facebookAuthSettings.AppId;
                options.AppSecret = facebookAuthSettings.AppSecret;

                // Request additional permissions (اختياري)
                options.Scope.Add("email");

                // Get extra fields like email and name
                options.Fields.Add("email");
                options.Fields.Add("name");

                // يضمن اننا ناخد الإيميل (أحياناً بيبقى مخفي)
                options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");

                //  التعامل مع فشل المصادقة (زي لما المستخدم يعمل Cancel)
                options.Events.OnRemoteFailure = context =>
                {
                    context.HandleResponse();
                    context.Response.Redirect("/Identity/Account/Login");
                    return Task.CompletedTask;
                };
            });

        services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequiredLength = 8;

            // User settings.
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
            options.User.RequireUniqueEmail = true;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // visit the below link for more information about Identity configuration
            // https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-configuration?view=aspnetcore-10.0
        });

        // Add ClaimsPrincipalFactory to add custom claims to the user (e.g. FullName)
        services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

        // Force immediate re-validation of the security stamp upon any user-related changes (e.g., password change, role update).
        // This ensures that if a user updates their password or role, they must re-authenticate immediately. 
        services.Configure<SecurityStampValidatorOptions>(option => option.ValidationInterval = TimeSpan.Zero);

        // Add EmailSender to the container of services
        services.AddTransient<IEmailSender, EmailSender>();

        // Add EmailBodyBuilder to the container of services
        services.AddTransient<IEmailBodyBuilder, EmailBodyBuilder>();

        // is used to add the MVC services to the container of services (it will add the controllers and views to the application) 
        services.AddControllersWithViews();

        // Add AutoMapper
        services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

        // Add Mail settings
        var mailSettings = builder.Configuration.GetSection(nameof(MailSettings)) ?? throw new InvalidOperationException("MailSettings section not found.");
        services.Configure<MailSettings>(mailSettings);

        // Add ExpressiveAnnotations
        services.AddExpressiveAnnotations();

        // Add Hangfire service
        builder.Services.AddHangfire(config =>
        {
            // Retrieve the database connection string from configuration
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            config.UseSqlServerStorage(connectionString);

        });
        // Add Hangfire server
        builder.Services.AddHangfireServer();


        // Add Authorization policy for AdminsOnly
        services.Configure<AuthorizationOptions>(options =>
        options.AddPolicy("AdminOnly", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireRole(AppRoles.Admin);
        }));

        // Add MVC services
        services.AddMvc(options =>
        {
            // Add Antiforgery token attribute to the application (will be added automatically to all POST requests)
            options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
        });

        return services;
    }
}
