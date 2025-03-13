using Movies.DataAccess.Models;

namespace Movies.DataAccess.Services.Interfaces
{
    public interface IGroupQuizParticipantService
    {
        // Adds a user as a participant in a quiz group.
        Task<GroupQuizParticipant> AddParticipantAsync(int groupQuizId, string userId);

        // Removes a participant from a quiz group.
        Task RemoveParticipantAsync(int participantId);

        // Retrieves all participants in a specific quiz group.
        Task<IEnumerable<GroupQuizParticipant>> GetParticipantsByGroupQuizAsync(int groupQuizId);
    }
}
