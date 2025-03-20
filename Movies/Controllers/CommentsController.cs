using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO;
using Movies.Shared.DTO.ModelDTOs;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController(ICommentService commentService, IMapper mapper) : ControllerBase
    {
        private readonly ICommentService _commentService = commentService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Retrieve all comments (and nested replies) for a specific content item.
        /// GET: /api/comments?contentId={contentId}
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetCommentsForContent([FromQuery] int contentId)
        {
            var comments = await _commentService.GetCommentsForContentAsync(contentId);
            var commentDtos = _mapper.Map<IEnumerable<CommentDto>>(comments);
            return Ok(commentDtos);
        }

        /// <summary>
        /// Add a new comment (or reply).
        /// POST: /api/comments
        /// </summary>
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
        /// (Optional) Retrieve a single comment by its ID.
        /// GET: /api/comments/{commentId}
        /// </summary>
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
        /// PUT: /api/comments/{commentId}
        /// </summary>
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
        /// DELETE: /api/comments/{commentId}
        /// </summary>
        [HttpDelete("{commentId:int}")]
        public async Task<IActionResult> SoftDeleteComment(int commentId)
        {
            try
            {
                await _commentService.SoftDeleteCommentAsync(commentId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
