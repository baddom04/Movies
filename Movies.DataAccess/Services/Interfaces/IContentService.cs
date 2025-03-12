using Movies.DataAccess.Models;
using Movies.DataAccess.Models.Enums;
using Movies.Shared.DTO;

namespace Movies.DataAccess.Services.Interfaces
{
    public interface IContentService
    {
        // Retrieves content by its ID.
        Task<Content> GetContentByIdAsync(int contentId);

        // Retrieves all available content.
        Task<IEnumerable<Content>> GetAllContentsAsync();

        // Searches content based on title, release year, type, or genre.
        Task<IEnumerable<Content>> SearchContentsAsync(string title, int? releaseYear, ContentType? type, int? genreId);

        // Creates a new content record.
        Task<Content> CreateContentAsync(Content content);

        // Updates existing content details.
        Task UpdateContentAsync(Content content);

        // Deletes content by its ID.
        Task DeleteContentAsync(int contentId);

        // Retrieves statistical data for a piece of content (e.g., average rating, total ratings, etc.).
        Task<ContentStatisticsDto> GetContentStatisticsAsync(int contentId);
    }

}
