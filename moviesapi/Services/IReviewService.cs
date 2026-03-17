using moviesapi.Models;

namespace moviesapi.Services;

public interface IReviewService
{
    IEnumerable<Review> GetReviewsByMovieId(int movieId);
    Review AddReview(ReviewCreateDto reviewDto);
}
