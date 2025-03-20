using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Models;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO;

namespace Movies.DataAccess.Services
{
    public class QuizVoteService(MoviesDbContext context) : IQuizVoteService
    {
        private readonly MoviesDbContext _context = context;

        public async Task<QuizVote> AddQuizVoteAsync(CreateQuizVoteDto quizVoteDto)
        {
            QuizVote quizVote = new()
            {
                UserId = quizVoteDto.UserId,
                ContentId = quizVoteDto.ContentId,
                Vote = quizVoteDto.Vote,
                QuizSessionId = quizVoteDto.QuizSessionId,
                VotedAt = DateTime.UtcNow
            };

            _context.QuizVotes.Add(quizVote);
            await _context.SaveChangesAsync();
            return quizVote;
        }

        public async Task<IEnumerable<QuizVote>> GetVotesForQuizSessionAsync(int quizSessionId)
        {
            return await _context.QuizVotes
                .Where(qv => qv.QuizSessionId == quizSessionId)
                .Include(qv => qv.User)
                .Include(qv => qv.Content)
                .ToListAsync();
        }

        public async Task<IEnumerable<QuizVote>> GetVotesForUserAsync(string userId)
        {
            return await _context.QuizVotes
                .Where(qv => qv.UserId == userId)
                .Include(qv => qv.QuizSession)
                .Include(qv => qv.Content)
                .ToListAsync();
        }

        public async Task DeleteQuizVoteAsync(int voteId)
        {
            var vote = await _context.QuizVotes
                .FirstOrDefaultAsync(qv => qv.Id == voteId)
                ?? throw new EntityNotFoundException();

            _context.QuizVotes.Remove(vote);
            await _context.SaveChangesAsync();
        }

        public async Task<QuizVote> GetQuizVoteByIdAsync(int voteId)
        {
            return await _context.QuizVotes.FindAsync(voteId) 
                ?? throw new EntityNotFoundException();
        }
    }
}
