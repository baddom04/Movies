using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO.ModelDTOs;

namespace Movies.Controllers
{
    /// <summary>
    /// Provides endpoints for genre management including retrieving all genres,
    /// retrieving a specific genre by ID, and fetching all content items associated with a genre.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController(IGenreService genreService, IMapper mapper) : ControllerBase
    {
        private readonly IGenreService _genreService = genreService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Retrieves all genres.
        /// </summary>
        /// <returns>An IActionResult containing an IEnumerable of GenreDto objects.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreService.GetAllGenresAsync();
            var genreDtos = _mapper.Map<IEnumerable<GenreDto>>(genres);
            return Ok(genreDtos);
        }

        /// <summary>
        /// Retrieves details for a specific genre by its ID.
        /// </summary>
        /// <param name="genreId">The unique identifier of the genre to retrieve.</param>
        /// <returns>An IActionResult containing the GenreDto if found, or a NotFound result if not found.</returns>
        [HttpGet("{genreId:int}")]
        public async Task<IActionResult> GetGenreById(int genreId)
        {
            try
            {
                var genre = await _genreService.GetGenreByIdAsync(genreId);
                var genreDto = _mapper.Map<GenreDto>(genre);
                return Ok(genreDto);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves all content items associated with a specific genre.
        /// </summary>
        /// <param name="genreId">The unique identifier of the genre for which to retrieve content items.</param>
        /// <returns>An IActionResult containing an IEnumerable of ContentDto objects.</returns>
        [HttpGet("{genreId:int}/contents")]
        public async Task<IActionResult> GetContentsByGenre(int genreId)
        {
            var contents = await _genreService.GetContentsByGenreAsync(genreId);
            var contentDtos = _mapper.Map<IEnumerable<ContentDto>>(contents);
            return Ok(contentDtos);
        }
    }
}
