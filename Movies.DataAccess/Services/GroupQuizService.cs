using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Models;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO;

namespace Movies.DataAccess.Services
{
    public class GroupQuizService(MoviesDbContext context) : IGroupQuizService
    {
        private readonly MoviesDbContext _context = context;

        public async Task<GroupQuiz> CreateGroupQuizAsync(CreateGroupQuizDto groupQuizDto)
        {
            GroupQuiz groupQuiz = new()
            {
                CreatedAt = DateTime.UtcNow,
                CreatorId = groupQuizDto.CreatorId,
                Name = groupQuizDto.Name,
                Code = groupQuizDto.Code,
            };

            _context.GroupQuizzes.Add(groupQuiz);
            await _context.SaveChangesAsync();
            return groupQuiz;
        }

        public async Task<GroupQuiz> GetGroupQuizByIdAsync(int groupQuizId)
        {
            var groupQuiz = await _context.GroupQuizzes
                .Include(gq => gq.Participants)
                .Include(gq => gq.QuizSessions)
                .FirstOrDefaultAsync(gq => gq.Id == groupQuizId);

            return groupQuiz ?? throw new EntityNotFoundException();
        }

        public async Task<IEnumerable<GroupQuiz>> GetGroupQuizzesForUserAsync(string userId)
        {
            var groupQuizzes = await _context.GroupQuizzes
                .Include(gq => gq.Participants)
                .Where(gq => gq.Participants.Any(p => p.UserId == userId))
                .ToListAsync();

            return groupQuizzes;
        }

        public async Task DeleteGroupQuizAsync(int groupQuizId)
        {
            var groupQuiz = await _context.GroupQuizzes
                .Include(gq => gq.Participants)
                .Include(gq => gq.QuizSessions)
                .FirstOrDefaultAsync(gq => gq.Id == groupQuizId) 
                ?? throw new EntityNotFoundException();

            _context.GroupQuizzes.Remove(groupQuiz);
            await _context.SaveChangesAsync();
        }
    }
}
