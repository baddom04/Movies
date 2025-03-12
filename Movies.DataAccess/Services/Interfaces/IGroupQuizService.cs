using Movies.DataAccess.Models;

namespace Movies.DataAccess.Services.Interfaces
{
    public interface IGroupQuizService
    {
        // Creates a new quiz group.
        Task<GroupQuiz> CreateGroupQuizAsync(GroupQuiz groupQuiz);

        // Retrieves a quiz group by its ID.
        Task<GroupQuiz> GetGroupQuizByIdAsync(int groupQuizId);

        // Retrieves all quiz groups for a user.
        Task<IEnumerable<GroupQuiz>> GetGroupQuizzesForUserAsync(int userId);

        // Deletes a quiz group.
        Task DeleteGroupQuizAsync(int groupQuizId);
    }
}
