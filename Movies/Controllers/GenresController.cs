using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO.ModelDTOs;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController(IGenreService genreService, IMapper mapper) : ControllerBase
    {
        private readonly IGenreService _genreService = genreService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Retrieves all genres.
        /// GET: /api/genres
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreService.GetAllGenresAsync();
            var genreDtos = _mapper.Map<IEnumerable<GenreDto>>(genres);
            return Ok(genreDtos);
        }

        /// <summary>
        /// Retrieves details for a specific genre by its ID.
        /// GET: /api/genres/{genreId}
        /// </summary>
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
        /// GET: /api/genres/{genreId}/contents
        /// </summary>
        [HttpGet("{genreId:int}/contents")]
        public async Task<IActionResult> GetContentsByGenre(int genreId)
        {
            var contents = await _genreService.GetContentsByGenreAsync(genreId);
            var contentDtos = _mapper.Map<IEnumerable<ContentDto>>(contents);
            return Ok(contentDtos);
        }
    }
}
