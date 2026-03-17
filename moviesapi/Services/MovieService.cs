using moviesapi.Models;
using System.Linq;

namespace moviesapi.Services;

public class MovieService : IMovieService
{
    private static readonly List<Movie> _movies = new List<Movie>
    {
        new Movie { Id = 1, Title = "The Shawshank Redemption", Genre = "Drama", ReleaseYear = 1994, AverageRating = 0 },
        new Movie { Id = 2, Title = "The Godfather", Genre = "Crime", ReleaseYear = 1972, AverageRating = 0 },
        new Movie { Id = 3, Title = "The Dark Knight", Genre = "Action", ReleaseYear = 2008, AverageRating = 0 },
        new Movie { Id = 4, Title = "Pulp Fiction", Genre = "Crime", ReleaseYear = 1994, AverageRating = 0 },
        new Movie { Id = 5, Title = "Forrest Gump", Genre = "Drama", ReleaseYear = 1994, AverageRating = 0 }
    };
    private static int _nextId = 6;
    private readonly IReviewService _reviewService;

    public MovieService(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    public IEnumerable<Movie> GetAll()
    {
        foreach (var movie in _movies)
        {
            movie.AverageRating = _reviewService.GetReviewsByMovieId(movie.Id).Any() ?
                                  _reviewService.GetReviewsByMovieId(movie.Id).Average(r => r.Rating) : 0;
        }
        return _movies;
    }

    public Movie? GetById(int id)
    {
        var movie = _movies.FirstOrDefault(m => m.Id == id);
        if (movie != null)
        {
            movie.AverageRating = _reviewService.GetReviewsByMovieId(movie.Id).Any() ?
                                  _reviewService.GetReviewsByMovieId(movie.Id).Average(r => r.Rating) : 0;
        }
        return movie;
    }

    public Movie Create(MovieCreateDto movieDto)
    {
        var movie = new Movie
        {
            Id = _nextId++,
            Title = movieDto.Title,
            Genre = movieDto.Genre,
            ReleaseYear = movieDto.ReleaseYear,
            AverageRating = 0 // New movies start with 0 rating
        };
        _movies.Add(movie);
        return movie;
    }

    public void Update(int id, MovieUpdateDto movieDto)
    {
        var movie = GetById(id); // GetById already calculates average rating
        if (movie is not null)
        {
            movie.Title = movieDto.Title;
            movie.Genre = movieDto.Genre;
            movie.ReleaseYear = movieDto.ReleaseYear;
            // AverageRating is recalculated on GetById, so no need to set here
        }
    }

    public void Delete(int id)
    {
        var movie = GetById(id);
        if (movie is not null)
        {
            _movies.Remove(movie);
        }
    }
}
