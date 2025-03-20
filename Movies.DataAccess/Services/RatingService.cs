using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Models;
using Movies.DataAccess.Services.Interfaces;

namespace Movies.DataAccess.Services
{
    public class RatingService(MoviesDbContext context) : IRatingService
    {
        private readonly MoviesDbContext _context = context;
        public async Task<Rating> AddRatingAsync(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return rating;
        }

        public async Task DeleteRatingAsync(int ratingId)
        {
            Rating toDelete = await _context.Ratings.FindAsync(ratingId)
                ?? throw new EntityNotFoundException();

            _context.Ratings.Remove(toDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<double> GetAverageRatingForContentAsync(int contentId)
        {
            IEnumerable<Rating> ratings = await GetRatingsForContentAsync(contentId);
            return ratings.Average(r => r.Value);
        }

        public async Task<Rating> GetRatingByIdAsync(int ratingId)
        {
            return await _context.Ratings.FindAsync(ratingId) 
                ?? throw new EntityNotFoundException();
        }

        public async Task<IEnumerable<Rating>> GetRatingsForContentAsync(int contentId)
        {
            return await _context.Ratings
                .Where(r => r.ContentId == contentId)
                .ToListAsync();
        }

        public async Task UpdateRatingAsync(Rating rating)
        {
            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
        }
    }
}
