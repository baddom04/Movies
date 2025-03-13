using Microsoft.AspNetCore.Identity;

namespace Movies.DataAccess.Models
{
    public class User : IdentityUser
    {
        public DateTime RegistrationDate { get; set; }
        public string? Bio { get; set; }                  // Rövid bemutatkozás (opcionális)

        // Navigációs tulajdonságok
        public virtual ICollection<Rating> Ratings { get; set; } = [];
        public virtual ICollection<Comment> Comments { get; set; } = [];
        public virtual ICollection<Favorite> Favorites { get; set; } = [];
        public virtual ICollection<WatchlistItem> WatchlistItems { get; set; } = [];

        // Csoportos kvíz résztvevői (ha tag a kvíz csoportban)
        public virtual ICollection<GroupQuizParticipant>? GroupQuizParticipants { get; set; }
        public virtual ICollection<QuizVote>? GroupQuizVotes { get; set; }
    }
}
