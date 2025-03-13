using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Models;
using Movies.DataAccess.Services.Interfaces;

namespace Movies.DataAccess.Services
{
    public class GroupQuizParticipantService(MoviesDbContext context) : IGroupQuizParticipantService
    {
        private readonly MoviesDbContext _context = context;

        public async Task<GroupQuizParticipant> AddParticipantAsync(int groupQuizId, string userId)
        {
            var participant = new GroupQuizParticipant
            {
                GroupQuizId = groupQuizId,
                UserId = userId,
                JoinedAt = DateTime.UtcNow
            };

            _context.GroupQuizParticipants.Add(participant);
            await _context.SaveChangesAsync();
            return participant;
        }

        public async Task RemoveParticipantAsync(int participantId)
        {
            GroupQuizParticipant participant = await _context.GroupQuizParticipants
                .FirstOrDefaultAsync(p => p.Id == participantId)
                ?? throw new EntityNotFoundException();

            _context.GroupQuizParticipants.Remove(participant);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GroupQuizParticipant>> GetParticipantsByGroupQuizAsync(int groupQuizId)
        {
            return await _context.GroupQuizParticipants
                .Where(p => p.GroupQuizId == groupQuizId)
                .Include(p => p.User)
                .ToListAsync();
        }
    }
}
