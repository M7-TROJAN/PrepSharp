using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PrepSharp.Web.Data.EntitiesConfigurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.Property(q => q.Text)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(q => q.Difficulty)
            .HasConversion<int>(); // عشان يتخزن Enum كـ int

        builder.HasOne(q => q.Category)
            .WithMany(c => c.Questions)
            .HasForeignKey(q => q.CategoryId);

        builder.HasOne(q => q.CreatedBy)
            .WithMany()
            .HasForeignKey(q => q.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}