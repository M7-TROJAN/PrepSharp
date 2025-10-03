namespace PrepSharp.Web.Models;

public class UserAnswer
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string UserId { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTime SubmitedOn { get; set; } = DateTime.UtcNow;
    public bool IsCorrect { get; set; }
    public string Feedback { get; set; } = string.Empty;

    public Question Question { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;

}
