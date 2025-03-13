using System.ComponentModel.DataAnnotations;

namespace Movies.DataAccess.Models
{
    public class GroupQuiz
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }                
        public required string CreatorId { get; set; }             
        public required string Code { get; set; }                
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public virtual User Creator { get; set; } = null!;
        public virtual ICollection<GroupQuizParticipant> Participants { get; set; } = [];
        public virtual ICollection<QuizSession> QuizSessions { get; set; } = [];
    }
}
