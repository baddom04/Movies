using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO.ModelDTOs;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistsController(IWatchlistService watchlistService, IMapper mapper) : ControllerBase
    {
        private readonly IWatchlistService _watchlistService = watchlistService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Adds a content item to the user's watchlist.
        /// POST: /api/watchlists
        /// Request body should contain JSON with "userId" and "contentId".
        /// </summary>
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
        /// GET: /api/watchlists/{userId}
        /// </summary>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWatchlistForUser(string userId)
        {
            var watchlistContents = await _watchlistService.GetWatchlistForUserAsync(userId);
            // Map Content entities to ContentDto objects
            var contentDtos = _mapper.Map<IEnumerable<ContentDto>>(watchlistContents);
            return Ok(contentDtos);
        }

        /// <summary>
        /// Removes a content item from the user's watchlist.
        /// DELETE: /api/watchlists/{userId}/{contentId}
        /// </summary>
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
