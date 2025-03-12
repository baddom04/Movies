namespace Movies.DataAccess.Models
{
    public class WatchlistItem
    {
        public int UserId { get; set; }
        public int ContentId { get; set; }
        public DateTime AddedAt { get; set; }           // Mikor lett hozzáadva a listához

        public User User { get; set; } = null!;
        public Content Content { get; set; } = null!;
    }
}
