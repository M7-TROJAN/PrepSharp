using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PrepSharp.Web.Data.EntitiesConfigurations;

public class ModelAnswerConfiguration : IEntityTypeConfiguration<ModelAnswer>
{
    public void Configure(EntityTypeBuilder<ModelAnswer> builder)
    {
        builder.Property(m => m.Text)
            .IsRequired()
            .HasMaxLength(4000);

        builder.HasOne(m => m.Question)
            .WithMany()
            .HasForeignKey(m => m.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}