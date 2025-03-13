namespace Movies.DataAccess.Models
{
    public class QuizVote
    {
        public int Id { get; set; }
        public int QuizSessionId { get; set; }
        public required string UserId { get; set; }
        public int ContentId { get; set; }             // Az a film/sorozat, amire szavaztak
        public bool Vote { get; set; }                 // true: szívesen megnézné, false: nem
        public DateTime VotedAt { get; set; }

        public virtual QuizSession QuizSession { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual Content Content { get; set; } = null!;
    }
}
