using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO;
using Movies.Shared.DTO.ModelDTOs;

namespace Movies.Controllers
{
    /// <summary>
    /// Manages quiz vote operations by providing endpoints to record a new quiz vote,
    /// retrieve votes for a specific quiz session, retrieve votes by a specific user,
    /// and delete a quiz vote.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class QuizVotesController(IQuizVoteService quizVoteService, IMapper mapper) : ControllerBase
    {
        private readonly IQuizVoteService _quizVoteService = quizVoteService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Records a user's vote for a particular content item within a quiz session.
        /// </summary>
        /// <param name="createQuizVoteDto">A DTO containing the details for the quiz vote (user ID, content ID, quiz session ID, and vote value).</param>
        /// <returns>A CreatedAtAction result containing the created QuizVoteDto.</returns>
        [HttpPost]
        public async Task<IActionResult> AddQuizVote([FromBody] CreateQuizVoteDto createQuizVoteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var quizVote = await _quizVoteService.AddQuizVoteAsync(createQuizVoteDto);
            var quizVoteDto = _mapper.Map<QuizVoteDto>(quizVote);
            // Optionally, if you implement a GetQuizVoteById endpoint, use CreatedAtAction.
            return CreatedAtAction(nameof(GetQuizVoteById), new { voteId = quizVoteDto.Id }, quizVoteDto);
        }

        /// <summary>
        /// (Optional) Retrieves a single quiz vote by its ID.
        /// </summary>
        /// <param name="voteId">The unique identifier of the quiz vote to retrieve.</param>
        /// <returns>An IActionResult containing the QuizVoteDto if found; otherwise, a NotFound result.</returns>
        [HttpGet("{voteId:int}")]
        public async Task<IActionResult> GetQuizVoteById(int voteId)
        {
            try
            {
                var vote = await _quizVoteService.GetQuizVoteByIdAsync(voteId);
                var voteDto = _mapper.Map<QuizVoteDto>(vote);
                return Ok(voteDto);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves all votes cast in a specific quiz session.
        /// </summary>
        /// <param name="quizSessionId">The unique identifier of the quiz session for which to retrieve votes.</param>
        /// <returns>An IActionResult containing an IEnumerable of QuizVoteDto objects.</returns>
        [HttpGet("quizsession/{quizSessionId:int}")]
        public async Task<IActionResult> GetVotesForQuizSession(int quizSessionId)
        {
            var votes = await _quizVoteService.GetVotesForQuizSessionAsync(quizSessionId);
            var voteDtos = _mapper.Map<IEnumerable<QuizVoteDto>>(votes);
            return Ok(voteDtos);
        }

        /// <summary>
        /// Retrieves all votes by a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user for which to retrieve votes.</param>
        /// <returns>An IActionResult containing an IEnumerable of QuizVoteDto objects.</returns>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetVotesForUser(string userId)
        {
            var votes = await _quizVoteService.GetVotesForUserAsync(userId);
            var voteDtos = _mapper.Map<IEnumerable<QuizVoteDto>>(votes);
            return Ok(voteDtos);
        }

        /// <summary>
        /// Deletes a quiz vote.
        /// </summary>
        /// <param name="voteId">The unique identifier of the quiz vote to delete.</param>
        /// <returns>An IActionResult with NoContent if deletion is successful; otherwise, a NotFound result.</returns>
        [HttpDelete("{voteId:int}")]
        public async Task<IActionResult> DeleteQuizVote(int voteId)
        {
            try
            {
                await _quizVoteService.DeleteQuizVoteAsync(voteId);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
