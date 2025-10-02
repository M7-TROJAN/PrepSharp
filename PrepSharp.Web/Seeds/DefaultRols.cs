using PrepSharp.Domain.Consts;

namespace PrepSharp.Web.Seeds
{
    public static class DefaultRols
    {
        public static async Task SeedRolsAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.Roles.AnyAsync())
            {
                await roleManager.CreateAsync(new IdentityRole(AppRoles.Admin));
                await roleManager.CreateAsync(new IdentityRole(AppRoles.User));
            }
        }
    }
}