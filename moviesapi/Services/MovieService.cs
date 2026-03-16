using moviesapi.Models;

namespace moviesapi.Services;

public class MovieService : IMovieService
{
    private static readonly List<Movie> _movies = new List<Movie>
    {
        new Movie { Id = 1, Title = "The Shawshank Redemption", Genre = "Drama", ReleaseYear = 1994 },
        new Movie { Id = 2, Title = "The Godfather", Genre = "Crime", ReleaseYear = 1972 },
        new Movie { Id = 3, Title = "The Dark Knight", Genre = "Action", ReleaseYear = 2008 },
        new Movie { Id = 4, Title = "Pulp Fiction", Genre = "Crime", ReleaseYear = 1994 },
        new Movie { Id = 5, Title = "Forrest Gump", Genre = "Drama", ReleaseYear = 1994 }
    };
    private static int _nextId = 6;

    public IEnumerable<Movie> GetAll() => _movies;

    public Movie? GetById(int id) => _movies.FirstOrDefault(m => m.Id == id);

    public Movie Create(MovieCreateDto movieDto)
    {
        var movie = new Movie
        {
            Id = _nextId++,
            Title = movieDto.Title,
            Genre = movieDto.Genre,
            ReleaseYear = movieDto.ReleaseYear
        };
        _movies.Add(movie);
        return movie;
    }

    public void Update(int id, MovieUpdateDto movieDto)
    {
        var movie = GetById(id);
        if (movie is not null)
        {
            movie.Title = movieDto.Title;
            movie.Genre = movieDto.Genre;
            movie.ReleaseYear = movieDto.ReleaseYear;
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
