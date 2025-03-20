using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO.ModelDTOs;

namespace Movies.Controllers
{
    /// <summary>
    /// Provides personalized content recommendations for users via an endpoint that returns
    /// recommended content items based on the user’s preferences or interactions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsController(IRecommendationService recommendationService, IMapper mapper) : ControllerBase
    {
        private readonly IRecommendationService _recommendationService = recommendationService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Retrieves personalized content recommendations for a user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user for whom to retrieve recommendations.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing an IEnumerable of <see cref="ContentDto"/> objects 
        /// representing the recommended content items.
        /// </returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetRecommendationsForUser(string userId)
        {
            var recommendations = await _recommendationService.GetRecommendationsForUserAsync(userId);
            var recommendationDtos = _mapper.Map<IEnumerable<ContentDto>>(recommendations);
            return Ok(recommendationDtos);
        }
    }
}
