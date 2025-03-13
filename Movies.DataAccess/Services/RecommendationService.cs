using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Models;
using Movies.DataAccess.Services.Interfaces;

namespace Movies.DataAccess.Services
{
    public class RecommendationService(MoviesDbContext context) : IRecommendationService
    {
        private readonly MoviesDbContext _context = context;
        public async Task<IEnumerable<Content>> GetRecommendationsForUserAsync(string userId)
        {
            return await _context.Contents.ToListAsync();
        }
    }
}
