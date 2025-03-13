using Movies.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Movies.DataAccess.Models
{
    public class Content
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public required string Title { get; set; }
        [MaxLength(255)]
        public required string Description { get; set; }
        public int ReleaseYear { get; set; }
        public double IMDBRating { get; set; }
        public required string TrailerUrl { get; set; }
        public required string PosterUrl { get; set; }
        public ContentType Type { get; set; }

        // Navigation properties
        public virtual ICollection<Rating> Ratings { get; set; } = [];
        public virtual ICollection<Comment> Comments { get; set; } = [];
        public virtual ICollection<ContentGenre> ContentGenres { get; set; } = [];
        public virtual ICollection<Favorite> FavoritedBy { get; set; } = [];
        public virtual ICollection<WatchlistItem> InWatchlists { get; set; } = [];
    }
}
