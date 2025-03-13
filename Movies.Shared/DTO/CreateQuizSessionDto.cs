namespace Movies.Shared.DTO
{
    public class CreateQuizSessionDto
    {
        public int GroupQuizId { get; init; }
        public required string Title { get; init; }
    }
}
