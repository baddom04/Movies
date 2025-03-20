using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Models;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO.ModelDTOs;

namespace Movies.Controllers
{
    /// <summary>
    /// Provides endpoints for managing ratings, including adding, updating, and deleting ratings,
    /// as well as retrieving all ratings for a specific content item and computing average ratings.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController(IRatingService ratingService, IMapper mapper) : ControllerBase
    {
        private readonly IRatingService _ratingService = ratingService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Adds a new rating.
        /// </summary>
        /// <param name="ratingDto">A RatingDto containing the user ID, content ID, and rating value.</param>
        /// <returns>A CreatedAtAction result with the newly created RatingDto.</returns>
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
        /// Retrieves a single rating by its ID.
        /// </summary>
        /// <param name="ratingId">The unique identifier of the rating to retrieve.</param>
        /// <returns>An Ok result containing the RatingDto if found; otherwise, NotFound.</returns>
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
        /// Updates an existing rating.
        /// </summary>
        /// <param name="ratingId">The unique identifier of the rating to update.</param>
        /// <param name="ratingDto">A RatingDto containing the updated rating information.</param>
        /// <returns>A NoContent result on success, or NotFound if the rating is not found.</returns>
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
        /// Deletes a rating.
        /// </summary>
        /// <param name="ratingId">The unique identifier of the rating to delete.</param>
        /// <returns>A NoContent result on successful deletion, or NotFound if the rating is not found.</returns>
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
        /// Retrieves all ratings for a specific content item.
        /// </summary>
        /// <param name="contentId">The unique identifier of the content item for which to retrieve ratings.</param>
        /// <returns>An Ok result containing an IEnumerable of RatingDto objects.</returns>
        [HttpGet("content/{contentId:int}")]
        public async Task<IActionResult> GetRatingsForContent(int contentId)
        {
            var ratings = await _ratingService.GetRatingsForContentAsync(contentId);
            var ratingDtos = _mapper.Map<IEnumerable<RatingDto>>(ratings);
            return Ok(ratingDtos);
        }

        /// <summary>
        /// Calculates and returns the average rating for a specific content item.
        /// </summary>
        /// <param name="contentId">The unique identifier of the content item for which to calculate the average rating.</param>
        /// <returns>An Ok result containing the average rating.</returns>
        [HttpGet("content/{contentId:int}/average")]
        public async Task<IActionResult> GetAverageRatingForContent(int contentId)
        {
            double averageRating = await _ratingService.GetAverageRatingForContentAsync(contentId);
            return Ok(new { AverageRating = averageRating });
        }
    }
}
