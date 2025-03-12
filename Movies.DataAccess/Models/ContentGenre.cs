﻿namespace Movies.DataAccess.Models
{
    public class ContentGenre
    {
        public int ContentId { get; set; }
        public int GenreId { get; set; }

        public Content Content { get; set; } = null!;
        public Genre Genre { get; set; } = null!;
    }
}
