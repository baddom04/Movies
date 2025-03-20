using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO.ModelDTOs;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController(IFavoriteService favoriteService, IMapper mapper) : ControllerBase
    {
        private readonly IFavoriteService _favoriteService = favoriteService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Add a new favorite.
        /// POST: /api/favorites
        /// Body: { "userId": "userId", "contentId": 123 }
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromBody] FavoriteDto favoriteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _favoriteService.AddFavoriteAsync(favoriteDto.UserId, favoriteDto.ContentId);
                return Ok("Favorite added successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieve all favorites for a specific user.
        /// GET: /api/favorites/{userId}
        /// </summary>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetFavoritesForUser(string userId)
        {
            var favoriteContents = await _favoriteService.GetFavoritesForUserAsync(userId);
            // Map the list of Content entities to ContentDto objects.
            var favoriteContentDtos = _mapper.Map<IEnumerable<ContentDto>>(favoriteContents);
            return Ok(favoriteContentDtos);
        }

        /// <summary>
        /// Remove a favorite.
        /// DELETE: /api/favorites/{userId}/{contentId}
        /// </summary>
        [HttpDelete("{userId}/{contentId}")]
        public async Task<IActionResult> RemoveFavorite(string userId, int contentId)
        {
            try
            {
                await _favoriteService.RemoveFavoriteAsync(userId, contentId);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
