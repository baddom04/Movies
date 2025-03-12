namespace Movies.DataAccess.Models
{
    public class GroupQuizParticipant
    {
        public int Id { get; set; }
        public int GroupQuizId { get; set; }
        public int UserId { get; set; }
        public DateTime JoinedAt { get; set; }

        public GroupQuiz GroupQuiz { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
