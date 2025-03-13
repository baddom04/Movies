namespace Movies.DataAccess.Models
{
    public class GroupQuiz
    {
        public int Id { get; set; }
        public required string Name { get; set; }                // Pl. "Film Est Quiz"
        public required string CreatorId { get; set; }              // A csoport létrehozójának azonosítója
        public required string Code { get; set; }                // Belépési kód (opcionális)
        public DateTime CreatedAt { get; set; }

        public virtual User Creator { get; set; } = null!;
        public virtual required ICollection<GroupQuizParticipant> Participants { get; set; }
        public virtual ICollection<QuizSession> QuizSessions { get; set; } = [];
    }
}
