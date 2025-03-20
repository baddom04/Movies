using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO;
using Movies.Shared.DTO.ModelDTOs;

namespace Movies.Controllers
{
    /// <summary>
    /// Handles comment operations including retrieving all comments (and nested replies)
    /// for a specific content item, adding new comments or replies, updating comments, and
    /// soft-deleting comments.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController(ICommentService commentService, IMapper mapper) : ControllerBase
    {
        private readonly ICommentService _commentService = commentService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Retrieve all comments (and nested replies) for a specific content item.
        /// </summary>
        /// <param name="contentId">The ID of the content item for which to retrieve comments.</param>
        /// <returns>An IActionResult containing the list of comments as CommentDto objects.</returns>
        [HttpGet]
        public async Task<IActionResult> GetCommentsForContent([FromQuery] int contentId)
        {
            var comments = await _commentService.GetCommentsForContentAsync(contentId);
            var commentDtos = _mapper.Map<IEnumerable<CommentDto>>(comments);
            return Ok(commentDtos);
        }

        /// <summary>
        /// Add a new comment (or reply).
        /// </summary>
        /// <param name="commentDto">The CreateCommentDto object containing the new comment details.</param>
        /// <returns>An IActionResult containing the created comment as a CommentDto.</returns>
        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdComment = await _commentService.AddCommentAsync(commentDto);
            var createdCommentDto = _mapper.Map<CommentDto>(createdComment);

            // If you have a GetCommentById endpoint, you can use CreatedAtAction.
            return CreatedAtAction(nameof(GetCommentById), new { commentId = createdCommentDto.Id }, createdCommentDto);
        }

        /// <summary>
        /// Retrieve a single comment by its ID.
        /// </summary>
        /// <param name="commentId">The unique identifier of the comment to retrieve.</param>
        /// <returns>An IActionResult containing the CommentDto if found.</returns>
        [HttpGet("{commentId:int}")]
        public async Task<IActionResult> GetCommentById(int commentId)
        {
            // This endpoint is optional. It requires that ICommentService includes a GetCommentByIdAsync method.
            try
            {
                var comment = await _commentService.GetCommentByIdAsync(commentId);
                var commentDto = _mapper.Map<CommentDto>(comment);
                return Ok(commentDto);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Update a comment.
        /// </summary>
        /// <param name="commentId">The unique identifier of the comment to update.</param>
        /// <param name="commentDto">An UpdateCommentDto object containing the updated comment details.</param>
        /// <returns>An IActionResult containing the updated CommentDto on success.</returns>
        [HttpPut("{commentId:int}")]
        public async Task<IActionResult> UpdateComment(int commentId, [FromBody] UpdateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (commentDto.Id != 0 && commentDto.Id != commentId)
                return BadRequest("Mismatched comment ID in request.");

            try
            {
                var updatedComment = await _commentService.UpdateCommentAsync(commentDto);
                var updatedCommentDto = _mapper.Map<CommentDto>(updatedComment);
                return Ok(updatedCommentDto);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Soft-delete a comment (e.g., set text to "[deleted]").
        /// </summary>
        /// <param name="commentId">The unique identifier of the comment to soft-delete.</param>
        /// <returns>An IActionResult with status NoContent if deletion is successful.</returns>
        [HttpDelete("{commentId:int}")]
        public async Task<IActionResult> SoftDeleteComment(int commentId)
        {
            try
            {
                await _commentService.SoftDeleteCommentAsync(commentId);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
