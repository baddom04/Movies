using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO;
using Movies.Shared.DTO.ModelDTOs;

namespace Movies.Controllers
{
    /// <summary>
    /// Handles operations for group quiz participants including adding a participant to a quiz group,
    /// retrieving all participants for a specific quiz group, retrieving a participant by ID, and removing a participant.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GroupQuizParticipantsController(IGroupQuizParticipantService participantService, IMapper mapper) : ControllerBase
    {
        private readonly IGroupQuizParticipantService _participantService = participantService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Adds a user as a participant to a quiz group.
        /// </summary>
        /// <param name="dto">The AddParticipantDto containing the GroupQuizId and the UserId to add as a participant.</param>
        /// <returns>A CreatedAtAction response containing the created participant as a GroupQuizParticipantDto.</returns>
        [HttpPost]
        public async Task<IActionResult> AddParticipant([FromBody] AddParticipantDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var participant = await _participantService.AddParticipantAsync(dto.GroupQuizId, dto.UserId);
            var participantDto = _mapper.Map<GroupQuizParticipantDto>(participant);
            // Optionally, if you have a GetParticipantById endpoint, use CreatedAtAction.
            return CreatedAtAction(nameof(GetParticipantById), new { participantId = participantDto.Id }, participantDto);
        }

        /// <summary>
        /// Retrieves a participant by their ID.
        /// </summary>
        /// <param name="participantId">The unique identifier of the participant.</param>
        /// <returns>An IActionResult containing the GroupQuizParticipantDto if found; otherwise, a NotFound result.</returns>
        [HttpGet("{participantId:int}")]
        public async Task<IActionResult> GetParticipantById(int participantId)
        {
            try
            {
                var participant = await _participantService.GetGroupQuizParticipantByIdAsync(participantId);
                var participantDto = _mapper.Map<GroupQuizParticipantDto>(participant);
                return Ok(participantDto);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves all participants in a specific quiz group.
        /// </summary>
        /// <param name="groupQuizId">The unique identifier of the quiz group.</param>
        /// <returns>An IActionResult containing an IEnumerable of GroupQuizParticipantDto objects.</returns>
        [HttpGet("groupquiz/{groupQuizId:int}")]
        public async Task<IActionResult> GetParticipantsByGroupQuiz(int groupQuizId)
        {
            var participants = await _participantService.GetParticipantsByGroupQuizAsync(groupQuizId);
            var participantDtos = _mapper.Map<IEnumerable<GroupQuizParticipantDto>>(participants);
            return Ok(participantDtos);
        }

        /// <summary>
        /// Removes a participant from a quiz group.
        /// </summary>
        /// <param name="participantId">The unique identifier of the participant to remove.</param>
        /// <returns>An IActionResult with NoContent on successful removal, or NotFound if the participant is not found.</returns>
        [HttpDelete("{participantId:int}")]
        public async Task<IActionResult> RemoveParticipant(int participantId)
        {
            try
            {
                await _participantService.RemoveParticipantAsync(participantId);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
