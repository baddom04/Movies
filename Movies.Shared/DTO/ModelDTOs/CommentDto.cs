namespace Movies.Shared.DTO.ModelDTOs
{
    public class CommentDto
    {
        public int Id { get; init; }
        public required string UserId { get; init; }
        public int ContentId { get; init; }
        public int? ParentCommentId { get; init; }
        public required string Text { get; init; }
        public DateTime CreatedAt { get; init; }
        public bool IsDeleted { get; init; }

        // Navigation properties
        public UserDto? User { get; init; } = null!;
        public CommentDto? ParentComment { get; init; }
        public ICollection<CommentDto>? Replies { get; init; }
    }
}
