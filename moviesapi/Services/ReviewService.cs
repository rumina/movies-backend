using moviesapi.Models;

namespace moviesapi.Services;

public class ReviewService : IReviewService
{
    private static readonly List<Review> _reviews = new List<Review>();
    private static int _nextId = 1;

    public IEnumerable<Review> GetReviewsByMovieId(int movieId)
    {
        return _reviews.Where(r => r.MovieId == movieId).OrderByDescending(r => r.DateCreated);
    }

    public Review AddReview(ReviewCreateDto reviewDto)
    {
        var review = new Review
        {
            Id = _nextId++,
            MovieId = reviewDto.MovieId,
            Rating = reviewDto.Rating,
            Comment = reviewDto.Comment,
            ReviewerName = reviewDto.ReviewerName,
            DateCreated = DateTime.UtcNow
        };
        _reviews.Add(review);
        return review;
    }
}
