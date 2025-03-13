namespace Movies.DataAccess.Models
{
    public class QuizSession
    {
        public int Id { get; set; }
        public int GroupQuizId { get; set; }
        public required string Title { get; set; }   // Pl. "Mai esti választások"
        public DateTime CreatedAt { get; set; }

        public virtual GroupQuiz GroupQuiz { get; set; } = null!;
        public virtual ICollection<QuizVote> QuizVotes { get; set; } = [];
    }
}
