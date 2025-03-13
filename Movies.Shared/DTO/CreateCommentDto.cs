namespace Movies.Shared.DTO
{
    public class CreateCommentDto
    {
        public required string UserId { get; init; }
        public int ContentId { get; init; }
        public int? ParentCommendId { get; init; }
        public required string Text { get; init; }
    }
}
