using Movies.DataAccess.Models.Enums;

namespace Movies.DataAccess.Models
{
    public class Content
    {
        public int Id { get; set; }
        public required string Title { get; set; }              // Cím
        public required string Description { get; set; }        // Rövid leírás
        public int ReleaseYear { get; set; }           // Kiadás éve
        public double IMDBRating { get; set; }         // Pl. IMDb értékelés
        public required string TrailerUrl { get; set; }         // Előzetes / trailer URL
        public required string PosterUrl { get; set; }          // Plakát kép URL
        public ContentType Type { get; set; }          // Film vagy Sorozat

        // Navigációs tulajdonságok
        public virtual ICollection<Rating> Ratings { get; set; } = [];
        public virtual ICollection<Comment> Comments { get; set; } = [];
        public virtual required ICollection<ContentGenre> ContentGenres { get; set; }
        public virtual ICollection<Favorite> FavoritedBy { get; set; } = [];
        public virtual ICollection<WatchlistItem> InWatchlists { get; set; } = [];
    }
}
