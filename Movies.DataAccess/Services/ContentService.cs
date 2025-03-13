using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Models;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO;
using Movies.Shared.Enums;

namespace Movies.DataAccess.Services
{
    public class ContentService(MoviesDbContext context) : IContentService
    {
        private readonly MoviesDbContext _context = context;

        public async Task<Content> GetContentByIdAsync(int contentId)
        {
            return await _context.Contents
                .Include(c => c.Ratings)
                .Include(c => c.Comments)
                .Include(c => c.ContentGenres)
                .FirstOrDefaultAsync(c => c.Id == contentId) 
                ?? throw new EntityNotFoundException();
        }

        public async Task<IEnumerable<Content>> GetAllContentsAsync()
        {
            return await _context.Contents.ToListAsync();
        }

        public async Task<IEnumerable<Content>> SearchContentsAsync(string search, int? releaseYear, ContentType? type, int? genreId)
        {
            IQueryable<Content> query = _context.Contents.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Title.Contains(search) || c.Description.Contains(search));
            }
            if (releaseYear.HasValue)
            {
                query = query.Where(c => c.ReleaseYear == releaseYear.Value);
            }
            if (type.HasValue)
            {
                query = query.Where(c => c.Type == type.Value);
            }
            if (genreId.HasValue)
            {
                query = query.Where(c => c.ContentGenres.Any(cg => cg.GenreId == genreId.Value));
            }

            return await query.ToListAsync();
        }

        public async Task<Content> CreateContentAsync(Content content)
        {
            _context.Contents.Add(content);
            await _context.SaveChangesAsync();
            return content;
        }

        public async Task UpdateContentAsync(Content content)
        {
            _context.Contents.Update(content);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteContentAsync(int contentId)
        {
            var content = await _context.Contents.FindAsync(contentId) 
                ?? throw new EntityNotFoundException();
            _context.Contents.Remove(content);

            await _context.SaveChangesAsync();
        }

        public async Task<ContentStatisticsDto> GetContentStatisticsAsync(int contentId)
        {
            var content = await _context.Contents
                .Include(c => c.Ratings)
                .Include(c => c.Comments)
                .FirstOrDefaultAsync(c => c.Id == contentId) 
                ?? throw new EntityNotFoundException();

            int totalRatings = content.Ratings.Count;
            double averageRating = totalRatings > 0 ? content.Ratings.Average(r => r.Value) : 0;
            int totalComments = content.Comments.Count;

            return new ContentStatisticsDto
            {
                ContentId = content.Id,
                TotalRatings = totalRatings,
                AverageRating = averageRating,
                TotalComments = totalComments
            };
        }
    }
}
