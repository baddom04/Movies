namespace Movies.Shared.DTO.ModelDTOs
{
    public class QuizSessionDto
    {
        public int Id { get; init; }
        public int GroupQuizId { get; init; }
        public required string Title { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
