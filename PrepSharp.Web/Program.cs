using Hangfire;
using Hangfire.Dashboard;
using PrepSharp.Web;
using PrepSharp.Web.filters;
using PrepSharp.Web.Seeds;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddWebServices(builder);

var app = builder.Build();


// Add middleware to prevent the application from being embedded in an iframe
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Frame-Options", "DENY");
    await next();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    // Add StatusCodePagesWithReExecute middleware to handle errors and show a custom error page
    app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


// Seed the database with default roles and users if they do not exist
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>(); // Get a scope factory

using var scope = scopeFactory.CreateScope(); // Create a scope (means a new service provider is created)

var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
await DefaultRols.SeedRolsAsync(roleManager);
await DefaultUsers.SeedAdminUserAsync(userManager);
// end seed the database

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    DashboardTitle = "LitraLand Dashboard",
    //IsReadOnlyFunc = (DashboardContext context) => true,
    Authorization = new IDashboardAuthorizationFilter[]
    {
       new HangfireAuthorizationFilter("AdminsOnly")
    }
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();