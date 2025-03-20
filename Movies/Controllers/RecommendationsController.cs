using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO.ModelDTOs;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsController(IRecommendationService recommendationService, IMapper mapper) : ControllerBase
    {
        private readonly IRecommendationService _recommendationService = recommendationService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Retrieves personalized content recommendations for a user.
        /// GET: /api/recommendations/{userId}
        /// </summary>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetRecommendationsForUser(string userId)
        {
            var recommendations = await _recommendationService.GetRecommendationsForUserAsync(userId);
            var recommendationDtos = _mapper.Map<IEnumerable<ContentDto>>(recommendations);
            return Ok(recommendationDtos);
        }
    }
}
