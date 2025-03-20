using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.DataAccess.Models;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO.ModelDTOs;
using Movies.Shared.Enums;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentsController(IContentService contentService, IMapper mapper) : ControllerBase
    {
        private readonly IContentService _contentService = contentService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Retrieve all content items (films/series).
        /// GET: /api/contents
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllContents()
        {
            var contents = await _contentService.GetAllContentsAsync();
            var contentDtos = _mapper.Map<IEnumerable<ContentDto>>(contents);
            return Ok(contentDtos);
        }

        /// <summary>
        /// Retrieve details for a specific content item by its ID.
        /// GET: /api/contents/{contentId}
        /// </summary>
        [HttpGet("{contentId:int}")]
        public async Task<IActionResult> GetContentById(int contentId)
        {
            try
            {
                var content = await _contentService.GetContentByIdAsync(contentId);
                var contentDto = _mapper.Map<ContentDto>(content);
                return Ok(contentDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Create a new content item.
        /// POST: /api/contents
        /// </summary>
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
        /// PUT: /api/contents/{contentId}
        /// </summary>
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
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Delete a content item.
        /// DELETE: /api/contents/{contentId}
        /// </summary>
        [HttpDelete("{contentId:int}")]
        public async Task<IActionResult> DeleteContent(int contentId)
        {
            try
            {
                await _contentService.DeleteContentAsync(contentId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Search content by query parameters (e.g., title, releaseYear, type, genreId).
        /// GET: /api/contents/search?title=...&releaseYear=...&type=...&genreId=...
        /// </summary>
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
        /// GET: /api/contents/{contentId}/statistics
        /// </summary>
        [HttpGet("{contentId:int}/statistics")]
        public async Task<IActionResult> GetContentStatistics(int contentId)
        {
            try
            {
                var stats = await _contentService.GetContentStatisticsAsync(contentId);
                return Ok(stats); // stats might already be a DTO or you map it if needed
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
