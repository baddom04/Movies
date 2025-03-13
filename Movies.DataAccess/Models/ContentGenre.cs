namespace Movies.DataAccess.Models
{
    public class ContentGenre
    {
        public int ContentId { get; set; }
        public int GenreId { get; set; }

        public virtual Content Content { get; set; } = null!;
        public virtual Genre Genre { get; set; } = null!;
    }
}
