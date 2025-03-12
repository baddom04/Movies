using Movies.DataAccess.Models;

namespace Movies.DataAccess.Services.Interfaces
{
    public interface IGenreService
    {
        // Retrieves all genres.
        Task<IEnumerable<Genre>> GetAllGenresAsync();

        // Retrieves a specific genre by ID.
        Task<Genre> GetGenreByIdAsync(int genreId);

        // Retrieves all content associated with a given genre.
        Task<IEnumerable<Content>> GetContentsByGenreAsync(int genreId);
    }

}
