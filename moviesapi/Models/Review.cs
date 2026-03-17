namespace moviesapi.Models;

public class Review
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public int Rating { get; set; } // 1-5 stars
    public string? Comment { get; set; }
    public string? ReviewerName { get; set; }
    public DateTime DateCreated { get; set; }
}
