using Movies.DataAccess.Models;

namespace Movies.DataAccess.Services.Interfaces
{
    public interface IWatchlistService
    {
        // Adds a content item to the user's watchlist.
        Task AddToWatchlistAsync(string userId, int contentId);

        // Removes a content item from the watchlist.
        Task RemoveFromWatchlistAsync(string userId, int contentId);

        // Retrieves all content items in a user's watchlist.
        Task<IEnumerable<Content>> GetWatchlistForUserAsync(string userId);
    }
}
