namespace Movies.DataAccess.Models
{
    public class GroupQuiz
    {
        public int Id { get; set; }
        public required string Name { get; set; }                // Pl. "Film Est Quiz"
        public int CreatorId { get; set; }              // A csoport létrehozójának azonosítója
        public required string Code { get; set; }                // Belépési kód (opcionális)
        public DateTime CreatedAt { get; set; }

        public User Creator { get; set; } = null!;
        public required ICollection<GroupQuizParticipant> Participants { get; set; }
        public ICollection<QuizSession> QuizSessions { get; set; } = [];
    }
}
