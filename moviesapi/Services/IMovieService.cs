using moviesapi.Models;

namespace moviesapi.Services;

public interface IMovieService
{
    IEnumerable<Movie> GetAll();
    Movie? GetById(int id);
    Movie Create(MovieCreateDto movieDto);
    void Update(int id, MovieUpdateDto movieDto);
    void Delete(int id);
}
