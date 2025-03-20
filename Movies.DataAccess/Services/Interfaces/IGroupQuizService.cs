using Movies.DataAccess.Models;
using Movies.Shared.DTO;

namespace Movies.DataAccess.Services.Interfaces
{
    public interface IGroupQuizService
    {
        // Creates a new quiz group.
        Task<GroupQuiz> CreateGroupQuizAsync(CreateGroupQuizDto groupQuiz);
    
        // Retrieves a quiz group by its ID.
        Task<GroupQuiz> GetGroupQuizByIdAsync(int groupQuizId);

        // Retrieves all quiz groups for a user.
        Task<IEnumerable<GroupQuiz>> GetGroupQuizzesForUserAsync(string userId);

        // Deletes a quiz group.
        Task DeleteGroupQuizAsync(int groupQuizId);
    }
}
