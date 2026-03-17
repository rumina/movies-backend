using Microsoft.AspNetCore.Mvc;
using Moq;
using moviesapi.Controllers;
using moviesapi.Models;
using moviesapi.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MoviesApi.Tests
{
    public class MoviesControllerTests
    {
        private readonly Mock<IMovieService> _mockMovieService;
        private readonly MoviesController _controller;

        public MoviesControllerTests()
        {
            _mockMovieService = new Mock<IMovieService>();
            _controller = new MoviesController(_mockMovieService.Object);
        }

        [Fact]
        public void GetMovies_ReturnsOkResult_WithListOfMovies()
        {
            // Arrange
            var movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Test Movie 1" },
                new Movie { Id = 2, Title = "Test Movie 2" }
            };
            _mockMovieService.Setup(service => service.GetAll()).Returns(movies);

            // Act
            var result = _controller.GetMovies();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Movie>>(actionResult.Value);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void GetMovie_ReturnsNotFoundResult_WhenMovieDoesNotExist()
        {
            // Arrange
            int testMovieId = 1;
            _mockMovieService.Setup(service => service.GetById(testMovieId)).Returns((Movie)null);

            // Act
            var result = _controller.GetMovie(testMovieId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetMovie_ReturnsOkResult_WithMovie_WhenMovieExists()
        {
            // Arrange
            int testMovieId = 1;
            var movie = new Movie { Id = testMovieId, Title = "Test Movie" };
            _mockMovieService.Setup(service => service.GetById(testMovieId)).Returns(movie);

            // Act
            var result = _controller.GetMovie(testMovieId);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsType<Movie>(actionResult.Value);
            Assert.Equal(testMovieId, model.Id);
        }

        [Fact]
        public void CreateMovie_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var createDto = new MovieCreateDto { Title = "New Movie" };
            var createdMovie = new Movie { Id = 1, Title = "New Movie" };
            _mockMovieService.Setup(service => service.Create(createDto)).Returns(createdMovie);

            // Act
            var result = _controller.CreateMovie(createDto);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetMovie", actionResult.ActionName);
            Assert.Equal(createdMovie.Id, actionResult.RouteValues["id"]);
            var model = Assert.IsType<Movie>(actionResult.Value);
            Assert.Equal(createdMovie.Title, model.Title);
        }

        [Fact]
        public void UpdateMovie_ReturnsNoContentResult_WhenMovieExists()
        {
            // Arrange
            int testMovieId = 1;
            var updateDto = new MovieUpdateDto { Title = "Updated Title" };
            var existingMovie = new Movie { Id = testMovieId, Title = "Original Title" };
            _mockMovieService.Setup(service => service.GetById(testMovieId)).Returns(existingMovie);

            // Act
            var result = _controller.UpdateMovie(testMovieId, updateDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _mockMovieService.Verify(s => s.Update(testMovieId, updateDto), Times.Once);
        }

        [Fact]
        public void DeleteMovie_ReturnsNoContentResult_WhenMovieExists()
        {
            // Arrange
            int testMovieId = 1;
            var existingMovie = new Movie { Id = testMovieId, Title = "Test Movie" };
            _mockMovieService.Setup(service => service.GetById(testMovieId)).Returns(existingMovie);

            // Act
            var result = _controller.DeleteMovie(testMovieId);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _mockMovieService.Verify(s => s.Delete(testMovieId), Times.Once);
        }
    }
}