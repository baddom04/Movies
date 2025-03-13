using Movies.DataAccess.Models;
using Movies.Shared.DTO;

namespace Movies.DataAccess.Services.Interfaces
{
    public interface IQuizVoteService
    {
        // Records a user's vote for a particular content item within a quiz session.
        Task<QuizVote> AddQuizVoteAsync(CreateQuizVoteDto quizVote);

        // Retrieves all votes cast in a specific quiz session.
        Task<IEnumerable<QuizVote>> GetVotesForQuizSessionAsync(int quizSessionId);

        // Retrieves all votes by a specific user.
        Task<IEnumerable<QuizVote>> GetVotesForUserAsync(string userId);

        // Deletes a vote (if necessary, e.g., for vote retraction).
        Task DeleteQuizVoteAsync(int voteId);
    }
}
