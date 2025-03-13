namespace Movies.Shared.DTO.ModelDTOs
{
    public class GenreDto
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public string? Description { get; init; }
    }
}
