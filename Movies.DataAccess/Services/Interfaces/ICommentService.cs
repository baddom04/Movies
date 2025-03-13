using Movies.DataAccess.Models;
using Movies.Shared.DTO;
using Movies.Shared.DTO.ModelDTOs;

namespace Movies.DataAccess.Services.Interfaces
{
    public interface ICommentService
    {
        // Adds a comment or reply.
        Task<Comment> AddCommentAsync(CreateCommentDto comment);

        // Retrieves comments (and nested replies) for a given content item.
        Task<IEnumerable<Comment>> GetCommentsForContentAsync(int contentId);

        // Updates the text of an existing comment.
        Task<Comment> UpdateCommentAsync(UpdateCommentDto comment);

        // Performs a soft-delete on a comment (e.g., sets Text = "[deleted]" and marks as deleted).
        Task SoftDeleteCommentAsync(int commentId);
    }
}
