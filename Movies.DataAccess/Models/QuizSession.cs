using System.ComponentModel.DataAnnotations;

namespace Movies.DataAccess.Models
{
    public class QuizSession
    {
        public int Id { get; set; }
        public int GroupQuizId { get; set; }
        [MaxLength(50)]
        public required string Title { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public virtual GroupQuiz GroupQuiz { get; set; } = null!;
        public virtual ICollection<QuizVote> QuizVotes { get; set; } = [];
    }
}
