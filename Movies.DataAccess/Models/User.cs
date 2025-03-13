using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Movies.DataAccess.Models
{
    public class User : IdentityUser
    {
        public DateTime RegistrationDate { get; set; }
        [MaxLength(255)]
        public string? Bio { get; set; }

        // Navigation properties
        public virtual ICollection<Rating> Ratings { get; set; } = [];
        public virtual ICollection<Comment> Comments { get; set; } = [];
        public virtual ICollection<Favorite> Favorites { get; set; } = [];
        public virtual ICollection<WatchlistItem> WatchlistItems { get; set; } = [];
        public virtual ICollection<GroupQuizParticipant>? GroupQuizParticipants { get; set; }
        public virtual ICollection<QuizVote>? GroupQuizVotes { get; set; }
    }
}
