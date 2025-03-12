namespace Movies.DataAccess.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public required string Name { get; set; }                // Pl. "Sci-fi", "Romantikus vígjáték"
        public string? Description { get; set; }         // Opcionális leírás

        public ICollection<ContentGenre> ContentGenres { get; set; } = [];
    }
}
