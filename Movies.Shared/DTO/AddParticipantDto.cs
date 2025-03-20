namespace Movies.Shared.DTO
{
    public class AddParticipantDto
    {
        public int GroupQuizId { get; set; }
        public required string UserId { get; set; }
    }
}
