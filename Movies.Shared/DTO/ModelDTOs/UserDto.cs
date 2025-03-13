namespace Movies.Shared.DTO.ModelDTOs
{
    public class UserDto
    {
        public required string Id { get; init; }
        public required string UserName { get; init; }
        public required string Email { get; init; }
        public required string Bio { get; init; }

        // Navigation properties
        public ICollection<ContentDto>? Favorites { get; init; }
        public ICollection<ContentDto>? WatchlistItems { get; init; }
    }
}
