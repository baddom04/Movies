using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Models;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO;

namespace Movies.DataAccess.Services
{
    public class QuizSessionService(MoviesDbContext context) : IQuizSessionService
    {
        private readonly MoviesDbContext _context = context;

        public async Task<QuizSession> CreateQuizSessionAsync(CreateQuizSessionDto quizSessionDto)
        {
            QuizSession quizSession = new()
            {
                GroupQuizId = quizSessionDto.GroupQuizId,
                Title = quizSessionDto.Title,
                CreatedAt = DateTime.UtcNow,
            };

            _context.QuizSessions.Add(quizSession);
            await _context.SaveChangesAsync();
            return quizSession;
        }

        public async Task<QuizSession> GetQuizSessionByIdAsync(int quizSessionId)
        {
            var quizSession = await _context.QuizSessions
                .Include(qs => qs.QuizVotes)
                .FirstOrDefaultAsync(qs => qs.Id == quizSessionId)
                ?? throw new EntityNotFoundException();

            return quizSession;
        }

        public async Task<IEnumerable<QuizSession>> GetQuizSessionsForGroupQuizAsync(int groupQuizId)
        {
            return await _context.QuizSessions
                .Where(qs => qs.GroupQuizId == groupQuizId)
                .Include(qs => qs.QuizVotes)
                .ToListAsync();
        }
    }
}
