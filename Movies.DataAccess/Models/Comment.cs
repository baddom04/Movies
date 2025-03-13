using System.ComponentModel.DataAnnotations;

namespace Movies.DataAccess.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public int ContentId { get; set; }
        public int? ParentCommentId { get; set; }

        [MaxLength(255)]
        public required string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        // Navigation properties
        public virtual User User { get; set; } = null!;
        public virtual Content Content { get; set; } = null!;
        public virtual Comment? ParentComment { get; set; }
        public virtual ICollection<Comment> Replies { get; set; } = [];
    }
}
