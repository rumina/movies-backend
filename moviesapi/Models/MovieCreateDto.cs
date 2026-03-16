namespace moviesapi.Models;

public class MovieCreateDto
{
    public string? Title { get; set; }
    public string? Genre { get; set; }
    public int ReleaseYear { get; set; }
}
