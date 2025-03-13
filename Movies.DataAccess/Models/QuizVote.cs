namespace Movies.DataAccess.Models
{
    public class QuizVote
    {
        public int Id { get; set; }
        public int QuizSessionId { get; set; }
        public required string UserId { get; set; }
        public int ContentId { get; set; }
        public bool Vote { get; set; }
        public DateTime VotedAt { get; set; }

        // Navigation properties
        public virtual QuizSession QuizSession { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual Content Content { get; set; } = null!;
    }
}
