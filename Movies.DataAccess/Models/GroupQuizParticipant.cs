namespace Movies.DataAccess.Models
{
    public class GroupQuizParticipant
    {
        public int Id { get; set; }
        public int GroupQuizId { get; set; }
        public required string UserId { get; set; }
        public DateTime JoinedAt { get; set; }

        public virtual GroupQuiz GroupQuiz { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
