using Movies.DataAccess.Models;

namespace Movies.DataAccess.Services.Interfaces
{
    public interface IRecommendationService
    {
        // Returns a list of recommended content for a given user.
        Task<IEnumerable<Content>> GetRecommendationsForUserAsync(int userId);
    }
}
