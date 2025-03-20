namespace Movies.Shared.DTO.ModelDTOs
{
    public class GroupQuizParticipantDto
    {
        public int Id { get; init; }
        public int GroupQuizId { get; init; }
        public required string UserId { get; init; }
        public DateTime JoinedAt { get; init; }
    }
}
