using Microsoft.AspNetCore.Mvc;
using moviesapi.Models;
using moviesapi.Services;
using System.Collections.Generic;

namespace moviesapi.Controllers;

[ApiController]
[Route("api/movies/{movieId}/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;
    private readonly IMovieService _movieService; // To check if movie exists

    public ReviewsController(IReviewService reviewService, IMovieService movieService)
    {
        _reviewService = reviewService;
        _movieService = movieService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Review>> GetMovieReviews(int movieId)
    {
        if (_movieService.GetById(movieId) == null)
        {
            return NotFound($"Movie with ID {movieId} not found.");
        }
        return Ok(_reviewService.GetReviewsByMovieId(movieId));
    }

    [HttpPost]
    public ActionResult<Review> AddMovieReview(int movieId, ReviewCreateDto reviewDto)
    {
        if (_movieService.GetById(movieId) == null)
        {
            return NotFound($"Movie with ID {movieId} not found.");
        }

        if (movieId != reviewDto.MovieId)
        {
            return BadRequest("MovieId in route and body do not match.");
        }

        var review = _reviewService.AddReview(reviewDto);
        return CreatedAtAction(nameof(GetMovieReviews), new { movieId = review.MovieId }, review);
    }
}
