namespace Movies.Shared.DTO
{
    public class CreateGroupQuizDto
    {
        public required string Name { get; init; }
        public required string CreatorId { get; init; }
        public required string Code { get; init; }
    }
}
