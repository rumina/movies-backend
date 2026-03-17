using Microsoft.AspNetCore.Mvc;
using Moq;
using moviesapi.Controllers;
using moviesapi.Models;
using moviesapi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MoviesApi.Tests
{
    public class ReviewsControllerTests
    {
        private readonly Mock<IReviewService> _mockReviewService;
        private readonly Mock<IMovieService> _mockMovieService;
        private readonly ReviewsController _controller;

        public ReviewsControllerTests()
        {
            _mockReviewService = new Mock<IReviewService>();
            _mockMovieService = new Mock<IMovieService>();
            _controller = new ReviewsController(_mockReviewService.Object, _mockMovieService.Object);
        }

        [Fact]
        public void GetMovieReviews_ReturnsNotFound_WhenMovieDoesNotExist()
        {
            // Arrange
            int movieId = 99;
            _mockMovieService.Setup(s => s.GetById(movieId)).Returns((Movie)null);

            // Act
            var result = _controller.GetMovieReviews(movieId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GetMovieReviews_ReturnsOkResult_WithListOfReviews()
        {
            // Arrange
            int movieId = 1;
            var movie = new Movie { Id = movieId, Title = "Test Movie" };
            var reviews = new List<Review>
            {
                new Review { Id = 1, MovieId = movieId, Rating = 5, Comment = "Great!", ReviewerName = "User1" },
                new Review { Id = 2, MovieId = movieId, Rating = 4, Comment = "Good!", ReviewerName = "User2" }
            };
            _mockMovieService.Setup(s => s.GetById(movieId)).Returns(movie);
            _mockReviewService.Setup(s => s.GetReviewsByMovieId(movieId)).Returns(reviews);

            // Act
            var result = _controller.GetMovieReviews(movieId);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Review>>(actionResult.Value);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void AddMovieReview_ReturnsNotFound_WhenMovieDoesNotExist()
        {
            // Arrange
            int movieId = 99;
            var reviewDto = new ReviewCreateDto { MovieId = movieId, Rating = 4, Comment = "Test", ReviewerName = "Tester" };
            _mockMovieService.Setup(s => s.GetById(movieId)).Returns((Movie)null);

            // Act
            var result = _controller.AddMovieReview(movieId, reviewDto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void AddMovieReview_ReturnsBadRequest_WhenMovieIdMismatch()
        {
            // Arrange
            int routeMovieId = 1;
            int bodyMovieId = 2;
            var movie = new Movie { Id = routeMovieId, Title = "Test Movie" };
            var reviewDto = new ReviewCreateDto { MovieId = bodyMovieId, Rating = 4, Comment = "Test", ReviewerName = "Tester" };
            _mockMovieService.Setup(s => s.GetById(routeMovieId)).Returns(movie);

            // Act
            var result = _controller.AddMovieReview(routeMovieId, reviewDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void AddMovieReview_ReturnsCreatedAtActionResult_WhenSuccessful()
        {
            // Arrange
            int movieId = 1;
            var movie = new Movie { Id = movieId, Title = "Test Movie" };
            var reviewDto = new ReviewCreateDto { MovieId = movieId, Rating = 4, Comment = "Test", ReviewerName = "Tester" };
            var createdReview = new Review { Id = 1, MovieId = movieId, Rating = 4, Comment = "Test", ReviewerName = "Tester", DateCreated = DateTime.UtcNow };

            _mockMovieService.Setup(s => s.GetById(movieId)).Returns(movie);
            _mockReviewService.Setup(s => s.AddReview(reviewDto)).Returns(createdReview);

            // Act
            var result = _controller.AddMovieReview(movieId, reviewDto);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(ReviewsController.GetMovieReviews), actionResult.ActionName);
            Assert.Equal(createdReview.MovieId, actionResult.RouteValues["movieId"]);
            var model = Assert.IsType<Review>(actionResult.Value);
            Assert.Equal(createdReview.Comment, model.Comment);
            _mockReviewService.Verify(s => s.AddReview(reviewDto), Times.Once);
        }
    }
}
