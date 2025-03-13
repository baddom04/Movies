namespace Movies.Shared.DTO
{
    public class CreateQuizVoteDto
    {
        public int QuizSessionId { get; init; }
        public required string UserId { get; init; }
        public int ContentId { get; init; }
        public bool Vote { get; init; }
    }
}
