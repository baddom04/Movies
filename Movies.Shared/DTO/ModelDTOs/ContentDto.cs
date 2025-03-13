using Movies.Shared.Enums;

namespace Movies.Shared.DTO.ModelDTOs
{
    public class ContentDto
    {
        public int Id { get; init; }
        public required string Title { get; init; }
        public required string Description { get; init; }
        public int ReleaseYear { get; init; }
        public double IMDBRating { get; init; }
        public required string TrailerUrl { get; init; }
        public required string PosterUrl { get; init; }
        public ContentType Type { get; init; }

        // Navigation properties
        public required ICollection<RatingDto> Ratings { get; init; }
        public required ICollection<CommentDto> Comments { get; init; }
        public required ICollection<GenreDto> ContentGenres { get; init; }
    }
}
