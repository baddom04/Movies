namespace Movies.Shared.DTO
{
    public class UpdateCommentDto
    {
        public int Id { get; init; }
        public required string Text { get; init; }
    }
}
