namespace Movies.Shared.DTO
{
    public class ContentStatisticsDto
    {
        // The unique identifier of the content (film/series)
        public int ContentId { get; init; }

        // Average rating based on user evaluations
        public double AverageRating { get; init; }

        // Total number of ratings submitted for the content
        public int TotalRatings { get; init; }

        // Total number of comments associated with the content
        public int TotalComments { get; init; }
    }
}
