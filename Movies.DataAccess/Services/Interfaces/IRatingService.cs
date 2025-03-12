using Movies.DataAccess.Models;

namespace Movies.DataAccess.Services.Interfaces
{
    public interface IRatingService
    {
        // Adds a rating to a piece of content.
        Task<Rating> AddRatingAsync(Rating rating);

        // Retrieves all ratings for specific content.
        Task<IEnumerable<Rating>> GetRatingsForContentAsync(int contentId);

        // Calculates and returns the average rating for content.
        Task<double> GetAverageRatingForContentAsync(int contentId);

        // Updates an existing rating.
        Task UpdateRatingAsync(Rating rating);

        // Deletes a rating.
        Task DeleteRatingAsync(int ratingId);
    }
}
