using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace PrepSharp.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // this will apply all configurations in the current assembly
            base.OnModelCreating(builder);
        }

    }
}