using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO;
using Movies.Shared.DTO.ModelDTOs;

namespace Movies.Controllers
{
    /// <summary>
    /// Manages group quiz operations including creating a new quiz group, retrieving a group quiz by ID,
    /// retrieving all quiz groups a user participates in, and deleting a quiz group.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GroupQuizzesController(IGroupQuizService groupQuizService, IMapper mapper) : ControllerBase
    {
        private readonly IGroupQuizService _groupQuizService = groupQuizService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Creates a new quiz group.
        /// </summary>
        /// <param name="groupQuizDto">A CreateGroupQuizDto containing the details required to create the quiz group.</param>
        /// <returns>A CreatedAtAction result containing the created GroupQuizDto.</returns>
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
        /// </summary>
        /// <param name="groupQuizId">The unique identifier of the quiz group.</param>
        /// <returns>An IActionResult containing the GroupQuizDto if found; otherwise, a NotFound result.</returns>
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
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose quiz groups are to be retrieved.</param>
        /// <returns>An IActionResult containing an IEnumerable of GroupQuizDto objects.</returns>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetGroupQuizzesForUser(string userId)
        {
            var groupQuizzes = await _groupQuizService.GetGroupQuizzesForUserAsync(userId);
            var groupQuizDtos = _mapper.Map<IEnumerable<GroupQuizDto>>(groupQuizzes);
            return Ok(groupQuizDtos);
        }

        /// <summary>
        /// Deletes a quiz group.
        /// </summary>
        /// <param name="groupQuizId">The unique identifier of the quiz group to delete.</param>
        /// <returns>An IActionResult with NoContent if deletion is successful; otherwise, NotFound if the group quiz does not exist.</returns>
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
