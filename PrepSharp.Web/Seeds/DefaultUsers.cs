using PrepSharp.Domain.Consts;

namespace PrepSharp.Web.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
        {
            // seed the admin user
            ApplicationUser Admin = new ApplicationUser
            {
                UserName = "trojan",
                Email = "trojan@PrepSharp.com",
                FirstName = "Mahmoud",
                LastName = "Mattar",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                DateOfBirth = new DateTime(1998, 3, 3),
            };

            // first check if the admin user already exists
            var user = await userManager.FindByEmailAsync(Admin.Email);

            // if the admin user does not exist, create it
            if (user is null)
            {
                await userManager.CreateAsync(Admin, "Admin@123");
                await userManager.AddToRoleAsync(Admin, AppRoles.Admin);
            }
        }
    }
}