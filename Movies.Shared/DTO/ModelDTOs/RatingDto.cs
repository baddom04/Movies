namespace Movies.Shared.DTO.ModelDTOs
{
    public class RatingDto
    {
        public int Id { get; init; }
        public required string UserId { get; init; }
        public int ContentId { get; init; }
        public int Value { get; init; }
        public DateTime DateRated { get; init; }

        // Navigation properties
        public UserDto? User { get; init; }
        public ContentDto? Content { get; init; }
    }
}
