namespace PrepSharp.Web.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public string? ImageUrl { get; set; }
    public string? ImageThumbnailUrl { get; set; }
    public string? Address { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedOn { get; set; }
}
