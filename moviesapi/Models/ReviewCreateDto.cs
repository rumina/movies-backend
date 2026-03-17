using System.ComponentModel.DataAnnotations;

namespace moviesapi.Models;

public class ReviewCreateDto
{
    [Required]
    public int MovieId { get; set; }
    [Required]
    [Range(1, 5)]
    public int Rating { get; set; } // 1-5 stars
    public string? Comment { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string? ReviewerName { get; set; }
}
