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
    public class GroupQuizParticipantsController(IGroupQuizParticipantService participantService, IMapper mapper) : ControllerBase
    {
        private readonly IGroupQuizParticipantService _participantService = participantService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Adds a user as a participant to a quiz group.
        /// POST: /api/groupquizparticipants
        /// </summary>
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
        /// (Optional) Retrieves a participant by their ID.
        /// GET: /api/groupquizparticipants/{participantId}
        /// </summary>
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
        /// GET: /api/groupquizparticipants/groupquiz/{groupQuizId}
        /// </summary>
        [HttpGet("groupquiz/{groupQuizId:int}")]
        public async Task<IActionResult> GetParticipantsByGroupQuiz(int groupQuizId)
        {
            var participants = await _participantService.GetParticipantsByGroupQuizAsync(groupQuizId);
            var participantDtos = _mapper.Map<IEnumerable<GroupQuizParticipantDto>>(participants);
            return Ok(participantDtos);
        }

        /// <summary>
        /// Removes a participant from a quiz group.
        /// DELETE: /api/groupquizparticipants/{participantId}
        /// </summary>
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
