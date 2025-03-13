using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Models;
using Movies.DataAccess.Services.Interfaces;

namespace Movies.DataAccess.Services
{
    public class WatchlistService(MoviesDbContext context) : IWatchlistService
    {
        private readonly MoviesDbContext _context = context;
        public async Task AddToWatchlistAsync(string userId, int contentId)
        {
            WatchlistItem? existing = await _context.WatchlistItems
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ContentId == contentId);

            if (existing is not null)
            {
                throw new InvalidOperationException("The provided data already exists");
            }

            WatchlistItem wli = new()
            {
                UserId = userId,
                ContentId = contentId,
                AddedAt = DateTime.UtcNow,
            };

            _context.WatchlistItems.Add(wli);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Content>> GetWatchlistForUserAsync(string userId)
        {
            return await _context.WatchlistItems
                .Where(wli => wli.UserId == userId)
                .Include(wli => wli.Content)
                .Select(wli => wli.Content)
                .ToListAsync();
        }

        public async Task RemoveFromWatchlistAsync(string userId, int contentId)
        {
            WatchlistItem toDelete = await _context.WatchlistItems
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ContentId == contentId)
                ?? throw new EntityNotFoundException();

            _context.WatchlistItems.Remove(toDelete);
            await _context.SaveChangesAsync();
        }
    }
}
