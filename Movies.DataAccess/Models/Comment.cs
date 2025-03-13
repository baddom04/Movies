namespace Movies.DataAccess.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public int ContentId { get; set; }
        public int? ParentCommentId { get; set; }       // Ha válasz egy másik kommentre
        public required string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        // Kapcsolatok
        public virtual User User { get; set; } = null!;
        public virtual Content Content { get; set; } = null!;
        public virtual Comment? ParentComment { get; set; }
        public virtual ICollection<Comment> Replies { get; set; } = [];
    }
}
