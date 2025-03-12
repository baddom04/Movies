namespace Movies.DataAccess.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }           // Egyedi felhasználónév
        public required string Email { get; set; }              // Egyedi email cím
        public required string PasswordHash { get; set; }         // Jelszó hash-elve
        public DateTime RegistrationDate { get; set; }
        public string? Bio { get; set; }                  // Rövid bemutatkozás (opcionális)

        // Navigációs tulajdonságok
        public ICollection<Rating> Ratings { get; set; } = [];
        public ICollection<Comment> Comments { get; set; } = [];
        public ICollection<Favorite> Favorites { get; set; } = [];
        public ICollection<WatchlistItem> WatchlistItems { get; set; } = [];

        // Csoportos kvíz résztvevői (ha tag a kvíz csoportban)
        public ICollection<GroupQuizParticipant>? GroupQuizParticipants { get; set; }
    }
}
