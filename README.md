# Movies API Backend

This is a simple ASP.NET Core Web API for managing movies and their reviews. It provides full CRUD (Create, Read, Update, Delete) functionality for movies, allows users to add and view reviews for each movie, and calculates the average rating. Data is stored in-memory for simplicity.

## Features

*   **Movie Management:**
    *   Create, retrieve, update, and delete movie records.
    *   Each movie includes Title, Genre, Release Year, and an automatically calculated Average Rating.
*   **Review System:**
    *   Add reviews (Rating, Comment, Reviewer Name) to specific movies.
    *   View all reviews for a given movie.
*   **In-Memory Data Storage:** All movie and review data is stored in-memory, meaning it will reset when the application restarts.
*   **Unit Tests:** Comprehensive unit tests for both `MoviesController` and `ReviewsController` using xUnit and Moq.

## Getting Started

### Prerequisites

*   [.NET SDK](https://dotnet.microsoft.com/download) (Version 10.0 or later recommended)

### Running the API

1.  Navigate to the backend project directory:
    ```bash
    cd /Users/rumina/Projects/my-workspace/movies-api/moviesapi
    ```
2.  Run the application:
    ```bash
    dotnet run
    ```
    The API will typically run on `http://localhost:5279` (or another available port if 5279 is in use).

### API Endpoints

All endpoints are accessible via `http://localhost:5279/api/`.

*   **Movies:**
    *   `GET /api/movies`: Get all movies.
    *   `GET /api/movies/{id}`: Get a movie by ID.
    *   `POST /api/movies`: Create a new movie.
    *   `PUT /api/movies/{id}`: Update an existing movie.
    *   `DELETE /api/movies/{id}`: Delete a movie.

*   **Reviews:**
    *   `GET /api/movies/{movieId}/reviews`: Get all reviews for a specific movie.
    *   `POST /api/movies/{movieId}/reviews`: Add a new review to a specific movie.

### Running Tests

1.  Navigate to the test project directory:
    ```bash
    cd /Users/rumina/Projects/my-workspace/movies-api/tests/MoviesApi.Tests
    ```
2.  Run all unit tests:
    ```bash
    dotnet test
    ```