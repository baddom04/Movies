namespace Movies.Shared.DTO.ModelDTOs
{
    public class GroupQuizDto
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public required string Code { get; init; }
        public required string CreatorId { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
