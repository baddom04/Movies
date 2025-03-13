using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Models;
using Movies.DataAccess.Services.Interfaces;

namespace Movies.DataAccess.Services
{
    public class GenreService(MoviesDbContext context) : IGenreService
    {
        private readonly MoviesDbContext _context = context;
        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<IEnumerable<Content>> GetContentsByGenreAsync(int genreId)
        {
            return await _context.ContentGenres
                .Include(cg => cg.Content)
                .Where(cg => cg.GenreId == genreId).Select(cg => cg.Content)
                .ToListAsync();
        }

        public async Task<Genre> GetGenreByIdAsync(int genreId)
        {
            return await _context.Genres.FindAsync(genreId) 
                ?? throw new EntityNotFoundException();
        }
    }
}
