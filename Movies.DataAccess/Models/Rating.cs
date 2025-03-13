namespace Movies.DataAccess.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public int ContentId { get; set; }
        public int Value { get; set; }                 // 1 és 10 közötti érték
        public DateTime DateRated { get; set; }

        // Kapcsolatok
        public virtual User User { get; set; } = null!;
        public virtual Content Content { get; set; } = null!;
    }
}
