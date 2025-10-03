using PrepSharp.Web.Core.Enums;

namespace PrepSharp.Web.Models;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public QuestionDifficulty Difficulty { get; set; }
    public int CategoryId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ApplicationUser CreatedBy { get; set; } = null!;
    public string CreatedByUserId { get; set; } = null!;

    public Category Category { get; set; } = null!;

}
