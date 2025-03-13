using System.ComponentModel.DataAnnotations;

namespace Movies.DataAccess.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public required string Name { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }

        // Navigation properties
        public virtual ICollection<ContentGenre> ContentGenres { get; set; } = [];
    }
}
