namespace PrepSharp.Web.Models;

public class ModelAnswer
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public int QuestionId { get; set; }

    public Question Question { get; set; } = null!;
}
