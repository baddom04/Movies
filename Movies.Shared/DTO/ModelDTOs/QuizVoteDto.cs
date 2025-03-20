namespace Movies.Shared.DTO.ModelDTOs
{
    public class QuizVoteDto
    {
        public int Id { get; init; }
        public int QuizSessionId { get; init; }
        public required string UserId { get; init; }
        public int ContentId { get; init; }
        public bool Vote { get; init; }
        public DateTime VotedAt { get; init; }
    }
}
