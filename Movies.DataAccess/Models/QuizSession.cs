namespace Movies.DataAccess.Models
{
    public class QuizSession
    {
        public int Id { get; set; }
        public int GroupQuizId { get; set; }
        public required string Title { get; set; }   // Pl. "Mai esti választások"
        public DateTime CreatedAt { get; set; }

        public GroupQuiz GroupQuiz { get; set; } = null!;
        public ICollection<QuizVote> QuizVotes { get; set; } = [];
    }
}
