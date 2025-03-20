using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO.ModelDTOs;

namespace Movies.Controllers
{
    /// <summary>
    /// Manages user favorites by providing endpoints to add a content item as a favorite,
    /// retrieve all favorite content items for a user, and remove a content item from the favorites.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController(IFavoriteService favoriteService, IMapper mapper) : ControllerBase
    {
        private readonly IFavoriteService _favoriteService = favoriteService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Add a new favorite.
        /// </summary>
        /// <param name="favoriteDto">The DTO containing the user ID and content ID to add as a favorite.</param>
        /// <returns>A success message if the favorite is added successfully.</returns>
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
        /// </summary>
        /// <param name="userId">The ID of the user whose favorites are to be retrieved.</param>
        /// <returns>An IEnumerable of ContentDto objects representing the user's favorite content items.</returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetFavoritesForUser(string userId)
        {
            var favoriteContents = await _favoriteService.GetFavoritesForUserAsync(userId);
            var favoriteContentDtos = _mapper.Map<IEnumerable<ContentDto>>(favoriteContents);
            return Ok(favoriteContentDtos);
        }

        /// <summary>
        /// Remove a favorite.
        /// </summary>
        /// <param name="userId">The ID of the user whose favorite is to be removed.</param>
        /// <param name="contentId">The ID of the content item to remove from the user's favorites.</param>
        /// <returns>A NoContent result if deletion is successful; otherwise, a NotFound result if the favorite does not exist.</returns>
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
