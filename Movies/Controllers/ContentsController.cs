using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Models;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO.ModelDTOs;
using Movies.Shared.Enums;

namespace Movies.Controllers
{
    /// <summary>
    /// Manages content operations (films/series), including retrieving all content items,
    /// retrieving a specific content item by ID, creating new content, updating existing content,
    /// deleting content, searching content, and retrieving content statistics.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContentsController(IContentService contentService, IMapper mapper) : ControllerBase
    {
        private readonly IContentService _contentService = contentService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Retrieve all content items (films/series).
        /// </summary>
        /// <returns>An IActionResult containing an IEnumerable of ContentDto objects.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllContents()
        {
            var contents = await _contentService.GetAllContentsAsync();
            var contentDtos = _mapper.Map<IEnumerable<ContentDto>>(contents);
            return Ok(contentDtos);
        }

        /// <summary>
        /// Retrieve details for a specific content item by its ID.
        /// </summary>
        /// <param name="contentId">The unique identifier of the content item.</param>
        /// <returns>An IActionResult containing the ContentDto if found; otherwise, a NotFound result.</returns>
        [HttpGet("{contentId:int}")]
        public async Task<IActionResult> GetContentById(int contentId)
        {
            try
            {
                var content = await _contentService.GetContentByIdAsync(contentId);
                var contentDto = _mapper.Map<ContentDto>(content);
                return Ok(contentDto);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Create a new content item.
        /// </summary>
        /// <param name="contentDto">The ContentDto object containing the details of the new content.</param>
        /// <returns>An IActionResult containing the created ContentDto with a 201 Created status.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateContent([FromBody] ContentDto contentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map DTO to the domain entity
            var contentEntity = _mapper.Map<Content>(contentDto);

            // Call the service to create the content
            var createdContent = await _contentService.CreateContentAsync(contentEntity);

            // Map back to DTO to return
            var createdContentDto = _mapper.Map<ContentDto>(createdContent);
            return CreatedAtAction(nameof(GetContentById),
                new { contentId = createdContentDto.Id }, createdContentDto);
        }

        /// <summary>
        /// Update an existing content item.
        /// </summary>
        /// <param name="contentId">The unique identifier of the content item to update.</param>
        /// <param name="contentDto">The ContentDto object containing updated details.</param>
        /// <returns>An IActionResult with NoContent on success; otherwise, a BadRequest or NotFound result.</returns>
        [HttpPut("{contentId:int}")]
        public async Task<IActionResult> UpdateContent(int contentId, [FromBody] ContentDto contentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Ensure the DTO's ID matches the route ID
            if (contentDto.Id != 0 && contentDto.Id != contentId)
                return BadRequest("Mismatched content ID in request.");

            // Map the DTO to the domain entity
            var contentEntity = _mapper.Map<Content>(contentDto);
            contentEntity.Id = contentId; // ensure correct ID

            try
            {
                await _contentService.UpdateContentAsync(contentEntity);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Delete a content item.
        /// </summary>
        /// <param name="contentId">The unique identifier of the content item to delete.</param>
        /// <returns>An IActionResult with NoContent on success; otherwise, a NotFound result.</returns>
        [HttpDelete("{contentId:int}")]
        public async Task<IActionResult> DeleteContent(int contentId)
        {
            try
            {
                await _contentService.DeleteContentAsync(contentId);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Search content by query parameters (e.g., title, releaseYear, type, genreId).
        /// </summary>
        /// <param name="title">Optional title keyword to search for.</param>
        /// <param name="releaseYear">Optional release year filter.</param>
        /// <param name="type">Optional content type filter (e.g., film or series).</param>
        /// <param name="genreId">Optional genre ID filter.</param>
        /// <returns>An IActionResult containing an IEnumerable of ContentDto objects matching the search criteria.</returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchContent(
            [FromQuery] string? title,
            [FromQuery] int? releaseYear,
            [FromQuery] ContentType? type,
            [FromQuery] int? genreId)
        {
            var results = await _contentService.SearchContentsAsync(title, releaseYear, type, genreId);
            var resultDtos = _mapper.Map<IEnumerable<ContentDto>>(results);
            return Ok(resultDtos);
        }

        /// <summary>
        /// Get statistical data (average rating, total ratings, total comments) for a content item.
        /// </summary>
        /// <param name="contentId">The unique identifier of the content item.</param>
        /// <returns>An IActionResult containing the statistics, or a NotFound result if the content is not found.</returns>
        [HttpGet("{contentId:int}/statistics")]
        public async Task<IActionResult> GetContentStatistics(int contentId)
        {
            try
            {
                var stats = await _contentService.GetContentStatisticsAsync(contentId);
                return Ok(stats); // stats might already be a DTO or you map it if needed
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
