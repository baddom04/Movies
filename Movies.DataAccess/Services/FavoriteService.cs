using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Models;
using Movies.DataAccess.Services.Interfaces;

namespace Movies.DataAccess.Services
{
    public class FavoriteService(MoviesDbContext context) : IFavoriteService
    {
        private readonly MoviesDbContext _context = context;
        public async Task AddFavoriteAsync(string userId, int contentId)
        {
            Favorite? existing = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ContentId == contentId);

            if (existing is not null)
            {
                throw new InvalidOperationException("The provided data already exists");
            }

            Favorite fav = new()
            {
                UserId = userId,
                ContentId = contentId,
                AddedAt = DateTime.UtcNow,
            };
    
            _context.Favorites.Add(fav);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Content>> GetFavoritesForUserAsync(string userId)
        {
            return await _context.Favorites
                .Include(fav => fav.Content)
                .Where(fav => fav.UserId == userId)
                .Select(fav => fav.Content)
                .ToListAsync();
        }

        public async Task RemoveFavoriteAsync(string userId, int contentId)
        {
            Favorite toDelete = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ContentId == contentId)
                ?? throw new EntityNotFoundException();

            _context.Favorites.Remove(toDelete);
            await _context.SaveChangesAsync();
        }
    }
}
