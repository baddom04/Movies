namespace Movies.DataAccess.Models
{
    public class WatchlistItem
    {
        public required string UserId { get; set; }
        public int ContentId { get; set; }
        public DateTime AddedAt { get; set; }           // Mikor lett hozzáadva a listához

        public virtual User User { get; set; } = null!;
        public virtual Content Content { get; set; } = null!;
    }
}
