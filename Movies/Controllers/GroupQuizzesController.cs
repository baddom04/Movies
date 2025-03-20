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
    public class GroupQuizzesController(IGroupQuizService groupQuizService, IMapper mapper) : ControllerBase
    {
        private readonly IGroupQuizService _groupQuizService = groupQuizService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Creates a new quiz group.
        /// POST: /api/groupquizzes
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateGroupQuiz([FromBody] CreateGroupQuizDto groupQuizDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var groupQuiz = await _groupQuizService.CreateGroupQuizAsync(groupQuizDto);
            var groupQuizDtoOut = _mapper.Map<GroupQuizDto>(groupQuiz);
            return CreatedAtAction(nameof(GetGroupQuizById), new { groupQuizId = groupQuizDtoOut.Id }, groupQuizDtoOut);
        }

        /// <summary>
        /// Retrieves a quiz group by its ID.
        /// GET: /api/groupquizzes/{groupQuizId}
        /// </summary>
        [HttpGet("{groupQuizId:int}")]
        public async Task<IActionResult> GetGroupQuizById(int groupQuizId)
        {
            try
            {
                var groupQuiz = await _groupQuizService.GetGroupQuizByIdAsync(groupQuizId);
                var groupQuizDto = _mapper.Map<GroupQuizDto>(groupQuiz);
                return Ok(groupQuizDto);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves all quiz groups for a user.
        /// GET: /api/groupquizzes/user/{userId}
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetGroupQuizzesForUser(string userId)
        {
            var groupQuizzes = await _groupQuizService.GetGroupQuizzesForUserAsync(userId);
            var groupQuizDtos = _mapper.Map<IEnumerable<GroupQuizDto>>(groupQuizzes);
            return Ok(groupQuizDtos);
        }

        /// <summary>
        /// Deletes a quiz group.
        /// DELETE: /api/groupquizzes/{groupQuizId}
        /// </summary>
        [HttpDelete("{groupQuizId:int}")]
        public async Task<IActionResult> DeleteGroupQuiz(int groupQuizId)
        {
            try
            {
                await _groupQuizService.DeleteGroupQuizAsync(groupQuizId);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
