using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PrepSharp.Web.Data.EntitiesConfigurations;

public class UserAnswerConfiguration : IEntityTypeConfiguration<UserAnswer>
{
    public void Configure(EntityTypeBuilder<UserAnswer> builder)
    {
        builder.Property(a => a.Text)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(a => a.Feedback)
            .HasMaxLength(2000);

        builder.HasOne(a => a.Question)
            .WithMany()
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}