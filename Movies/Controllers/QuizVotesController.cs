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
    public class QuizVotesController(IQuizVoteService quizVoteService, IMapper mapper) : ControllerBase
    {
        private readonly IQuizVoteService _quizVoteService = quizVoteService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Records a user's vote for a particular content item within a quiz session.
        /// POST: /api/quizvotes
        /// </summary>
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
        /// (Optional) Retrieve a single quiz vote by its ID.
        /// GET: /api/quizvotes/{voteId}
        /// </summary>
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
        /// GET: /api/quizvotes/quizsession/{quizSessionId}
        /// </summary>
        [HttpGet("quizsession/{quizSessionId:int}")]
        public async Task<IActionResult> GetVotesForQuizSession(int quizSessionId)
        {
            var votes = await _quizVoteService.GetVotesForQuizSessionAsync(quizSessionId);
            var voteDtos = _mapper.Map<IEnumerable<QuizVoteDto>>(votes);
            return Ok(voteDtos);
        }

        /// <summary>
        /// Retrieves all votes by a specific user.
        /// GET: /api/quizvotes/user/{userId}
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetVotesForUser(string userId)
        {
            var votes = await _quizVoteService.GetVotesForUserAsync(userId);
            var voteDtos = _mapper.Map<IEnumerable<QuizVoteDto>>(votes);
            return Ok(voteDtos);
        }

        /// <summary>
        /// Deletes a quiz vote.
        /// DELETE: /api/quizvotes/{voteId}
        /// </summary>
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
