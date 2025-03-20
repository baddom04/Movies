using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO.ModelDTOs;

namespace Movies.Controllers
{
    /// <summary>
    /// Manages user watchlists with endpoints to add a content item to the watchlist,
    /// retrieve a user's watchlist, and remove a content item from the watchlist.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistsController(IWatchlistService watchlistService, IMapper mapper) : ControllerBase
    {
        private readonly IWatchlistService _watchlistService = watchlistService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Adds a content item to the user's watchlist.
        /// </summary>
        /// <param name="watchlistItemDto">
        /// A <see cref="WatchlistItemDto"/> containing the user ID and content ID of the item to be added.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating success with a confirmation message, or a BadRequest with an error message.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> AddToWatchlist([FromBody] WatchlistItemDto watchlistItemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _watchlistService.AddToWatchlistAsync(watchlistItemDto.UserId, watchlistItemDto.ContentId);
                return Ok("Content added to watchlist successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves the watchlist for a given user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose watchlist is to be retrieved.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing an <see cref="IEnumerable{ContentDto}"/> representing the user's watchlist.
        /// </returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWatchlistForUser(string userId)
        {
            var watchlistContents = await _watchlistService.GetWatchlistForUserAsync(userId);
            // Map Content entities to ContentDto objects.
            var contentDtos = _mapper.Map<IEnumerable<ContentDto>>(watchlistContents);
            return Ok(contentDtos);
        }

        /// <summary>
        /// Removes a content item from the user's watchlist.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="contentId">The unique identifier of the content item to remove.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> with a NoContent result if deletion is successful,
        /// or a NotFound result if the item does not exist.
        /// </returns>
        [HttpDelete("{userId}/{contentId}")]
        public async Task<IActionResult> RemoveFromWatchlist(string userId, int contentId)
        {
            try
            {
                await _watchlistService.RemoveFromWatchlistAsync(userId, contentId);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
