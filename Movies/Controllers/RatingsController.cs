using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DataAccess.Models;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO.ModelDTOs;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController(IRatingService ratingService, IMapper mapper) : ControllerBase
    {
        private readonly IRatingService _ratingService = ratingService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Add a rating (body includes userId, contentId, rating value).
        /// POST: /api/ratings
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddRating([FromBody] RatingDto ratingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map DTO to the domain entity
            var ratingEntity = _mapper.Map<Rating>(ratingDto);

            // Call the service to add the rating
            var createdRating = await _ratingService.AddRatingAsync(ratingEntity);

            // Map back to DTO
            var createdRatingDto = _mapper.Map<RatingDto>(createdRating);

            // Return 201 Created with the newly created resource
            return CreatedAtAction(nameof(GetRatingById), new { ratingId = createdRatingDto.Id }, createdRatingDto);
        }

        /// <summary>
        /// (Optional) Retrieve a single rating by ID if you want a direct fetch endpoint.
        /// GET: /api/ratings/{ratingId}
        /// </summary>
        [HttpGet("{ratingId:int}")]
        public async Task<IActionResult> GetRatingById(int ratingId)
        {
            var rating = await _ratingService.GetRatingByIdAsync(ratingId);
            if (rating == null)
                return NotFound();

            var ratingDto = _mapper.Map<RatingDto>(rating);
            return Ok(ratingDto);
        }

        /// <summary>
        /// Update an existing rating.
        /// PUT: /api/ratings/{ratingId}
        /// </summary>
        [HttpPut("{ratingId:int}")]
        public async Task<IActionResult> UpdateRating(int ratingId, [FromBody] RatingDto ratingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Ensure the DTO's ID matches the route ID (if you enforce that)
            if (ratingDto.Id != 0 && ratingDto.Id != ratingId)
                return BadRequest("Mismatched rating ID in request.");

            var ratingEntity = _mapper.Map<Rating>(ratingDto);
            ratingEntity.Id = ratingId; // ensure correct ID

            try
            {
                await _ratingService.UpdateRatingAsync(ratingEntity);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Delete a rating.
        /// DELETE: /api/ratings/{ratingId}
        /// </summary>
        [HttpDelete("{ratingId:int}")]
        public async Task<IActionResult> DeleteRating(int ratingId)
        {
            try
            {
                await _ratingService.DeleteRatingAsync(ratingId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Retrieve all ratings for specific content.
        /// GET: /api/ratings/content/{contentId}
        /// </summary>
        [HttpGet("content/{contentId:int}")]
        public async Task<IActionResult> GetRatingsForContent(int contentId)
        {
            var ratings = await _ratingService.GetRatingsForContentAsync(contentId);
            var ratingDtos = _mapper.Map<IEnumerable<RatingDto>>(ratings);
            return Ok(ratingDtos);
        }

        /// <summary>
        /// Calculate and return the average rating for specific content.
        /// GET: /api/ratings/content/{contentId}/average
        /// </summary>
        [HttpGet("content/{contentId:int}/average")]
        public async Task<IActionResult> GetAverageRatingForContent(int contentId)
        {
            double averageRating = await _ratingService.GetAverageRatingForContentAsync(contentId);
            return Ok(new { AverageRating = averageRating });
        }
    }
}
