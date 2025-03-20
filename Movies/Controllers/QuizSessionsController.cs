using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO;
using Movies.Shared.DTO.ModelDTOs;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizSessionsController(IQuizSessionService quizSessionService, IMapper mapper) : ControllerBase
    {
        private readonly IQuizSessionService _quizSessionService = quizSessionService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Creates a new quiz session for a group quiz.
        /// POST: /api/quizsessions
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateQuizSession([FromBody] CreateQuizSessionDto quizSessionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var quizSession = await _quizSessionService.CreateQuizSessionAsync(quizSessionDto);
            var quizSessionDtoOut = _mapper.Map<QuizSessionDto>(quizSession);
            return CreatedAtAction(nameof(GetQuizSessionById), new { quizSessionId = quizSessionDtoOut.Id }, quizSessionDtoOut);
        }

        /// <summary>
        /// Retrieves a quiz session by its ID.
        /// GET: /api/quizsessions/{quizSessionId}
        /// </summary>
        [HttpGet("{quizSessionId:int}")]
        public async Task<IActionResult> GetQuizSessionById(int quizSessionId)
        {
            try
            {
                var quizSession = await _quizSessionService.GetQuizSessionByIdAsync(quizSessionId);
                var quizSessionDto = _mapper.Map<QuizSessionDto>(quizSession);
                return Ok(quizSessionDto);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves all quiz sessions for a specific group quiz.
        /// GET: /api/quizsessions/groupquiz/{groupQuizId}
        /// </summary>
        [HttpGet("groupquiz/{groupQuizId:int}")]
        public async Task<IActionResult> GetQuizSessionsForGroupQuiz(int groupQuizId)
        {
            var quizSessions = await _quizSessionService.GetQuizSessionsForGroupQuizAsync(groupQuizId);
            var quizSessionsDto = _mapper.Map<IEnumerable<QuizSessionDto>>(quizSessions);
            return Ok(quizSessionsDto);
        }   
    }
}
