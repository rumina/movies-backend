using Microsoft.AspNetCore.Mvc;
using moviesapi.Models;
using moviesapi.Services;

namespace moviesapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Movie>> GetMovies()
    {
        return Ok(_movieService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Movie> GetMovie(int id)
    {
        var movie = _movieService.GetById(id);
        if (movie == null)
        {
            return NotFound();
        }
        return Ok(movie);
    }

    [HttpPost]
    public ActionResult<Movie> CreateMovie(MovieCreateDto movieDto)
    {
        var movie = _movieService.Create(movieDto);
        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMovie(int id, MovieUpdateDto movieDto)
    {
        var movie = _movieService.GetById(id);
        if (movie == null)
        {
            return NotFound();
        }
        _movieService.Update(id, movieDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(int id)
    {
        var movie = _movieService.GetById(id);
        if (movie == null)
        {
            return NotFound();
        }
        _movieService.Delete(id);
        return NoContent();
    }
}
