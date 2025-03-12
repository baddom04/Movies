using Movies.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DataAccess.Services.Interfaces
{
    public interface IFavoriteService
    {
        // Adds a content item to the user's favorites.
        Task AddFavoriteAsync(int userId, int contentId);

        // Removes a content item from the user's favorites.
        Task RemoveFavoriteAsync(int userId, int contentId);

        // Retrieves the list of favorite content for a user.
        Task<IEnumerable<Content>> GetFavoritesForUserAsync(int userId);
    }

}
