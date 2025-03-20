using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Models;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO;

namespace Movies.DataAccess.Services
{
    public class CommentService(MoviesDbContext context) : ICommentService
    {
        private readonly MoviesDbContext _context = context;

        public async Task<Comment> AddCommentAsync(CreateCommentDto comment)
        {
            Comment newComment = new()
            {
                UserId = comment.UserId,
                ContentId = comment.ContentId,
                ParentCommentId = comment.ParentCommendId,
                Text = comment.Text,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.Comments.Add(newComment);
            await _context.SaveChangesAsync();
            return newComment;
        }

        public async Task<Comment> GetCommentByIdAsync(int commentId)
        {
            return await _context.Comments.FindAsync(commentId) 
                ?? throw new EntityNotFoundException();
        }

        public async Task<IEnumerable<Comment>> GetCommentsForContentAsync(int contentId)
        {
            return await _context.Comments
            .Include(c => c.User)
            .Include(c => c.Replies)
            .Where(c => c.ContentId == contentId && c.ParentCommentId == null)
            .ToListAsync();
        }

        public async Task SoftDeleteCommentAsync(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId)
                ?? throw new EntityNotFoundException();

            comment.Text = "[deleted]";

            await _context.SaveChangesAsync();
        }

        public async Task<Comment> UpdateCommentAsync(UpdateCommentDto updatedComment)
        {
            var comment = await _context.Comments.FindAsync(updatedComment.Id)
                ?? throw new EntityNotFoundException();

            comment.Text = updatedComment.Text;

            await _context.SaveChangesAsync();
            return comment;
        }
    }
}
