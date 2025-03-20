using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO;
using Movies.Shared.DTO.ModelDTOs;

namespace Movies.Controllers
{
    /// <summary>
    /// Provides endpoints for managing quiz sessions within a group quiz including creating a new quiz session,
    /// retrieving a quiz session by its ID, and retrieving all quiz sessions for a specific group quiz.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class QuizSessionsController(IQuizSessionService quizSessionService, IMapper mapper) : ControllerBase
    {
        private readonly IQuizSessionService _quizSessionService = quizSessionService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Creates a new quiz session for a group quiz.
        /// </summary>
        /// <param name="quizSessionDto">
        /// A CreateQuizSessionDto object containing the necessary details (such as GroupQuizId and Title) to create a quiz session.
        /// </param>
        /// <returns>
        /// A CreatedAtAction result containing the newly created QuizSessionDto.
        /// </returns>
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
        /// </summary>
        /// <param name="quizSessionId">The unique identifier of the quiz session to retrieve.</param>
        /// <returns>
        /// An IActionResult containing the QuizSessionDto if found; otherwise, a NotFound result.
        /// </returns>
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
        /// </summary>
        /// <param name="groupQuizId">The unique identifier of the group quiz for which to retrieve quiz sessions.</param>
        /// <returns>
        /// An IActionResult containing an IEnumerable of QuizSessionDto objects.
        /// </returns>
        [HttpGet("groupquiz/{groupQuizId:int}")]
        public async Task<IActionResult> GetQuizSessionsForGroupQuiz(int groupQuizId)
        {
            var quizSessions = await _quizSessionService.GetQuizSessionsForGroupQuizAsync(groupQuizId);
            var quizSessionsDto = _mapper.Map<IEnumerable<QuizSessionDto>>(quizSessions);
            return Ok(quizSessionsDto);
        }
    }
}
