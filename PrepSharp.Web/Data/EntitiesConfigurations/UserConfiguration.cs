using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PrepSharp.Web.Data.EntitiesConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName)
            .HasMaxLength(100);
        builder.Property(u => u.LastName)
            .HasMaxLength(100);

        // prevent duplicated email and username
        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.UserName).IsUnique();

        // prevent duplicated phone number if not null
        builder.HasIndex(u => u.PhoneNumber)
            .IsUnique()
            .HasDatabaseName("IX_ApplicationUser_PhoneNumber")
            .HasFilter("[PhoneNumber] IS NOT NULL"); // only unique if not null (e.g. for users who don't have a phone number)

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(20);
    }
}