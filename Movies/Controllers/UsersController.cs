namespace Movies.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class UsersController(IUserService userService, IMapper mapper) : ControllerBase
    //{
    //    private readonly IUserService _userService = userService;
    //    private readonly IMapper _mapper = mapper;

    //    // POST: api/users/register
    //    [HttpPost("register")]
    //    public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
    //    {
    //        if (!ModelState.IsValid)
    //            return BadRequest(ModelState);

    //        try
    //        {
    //            var user = await _userService.RegisterUserAsync(registrationDto);
    //            var userDto = _mapper.Map<UserDto>(user);
    //            return CreatedAtAction(nameof(GetUserById), new { userId = user.Id }, userDto);
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(ex.Message);
    //        }
    //    }

    //    // POST: api/users/login
    //    [HttpPost("login")]
    //    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    //    {
    //        if (!ModelState.IsValid)
    //            return BadRequest(ModelState);

    //        try
    //        {
    //            var user = await _userService.LoginUserAsync(loginDto);
    //            var userDto = _mapper.Map<UserDto>(user);
    //            return Ok(userDto);
    //        }
    //        catch (UnauthorizedAccessException ex)
    //        {
    //            return Unauthorized(ex.Message);
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(ex.Message);
    //        }
    //    }

    //    // GET: api/users/{userId}
    //    [HttpGet("{userId}")]
    //    public async Task<IActionResult> GetUserById(string userId)
    //    {
    //        try
    //        {
    //            var user = await _userService.GetUserByIdAsync(userId);
    //            var userDto = _mapper.Map<UserDto>(user);
    //            return Ok(userDto);
    //        }
    //        catch (Exception ex)
    //        {
    //            return NotFound(ex.Message);
    //        }
    //    }

    //    // PUT: api/users/{userId}
    //    [HttpPut("{userId}")]
    //    public async Task<IActionResult> UpdateUserProfile(string userId, [FromBody] UserProfileUpdateDto profileDto)
    //    {
    //        if (!ModelState.IsValid)
    //            return BadRequest(ModelState);

    //        try
    //        {
    //            await _userService.UpdateUserProfileAsync(userId, profileDto);
    //            return NoContent();
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(ex.Message);
    //        }
    //    }

    //    // GET: api/users
    //    [HttpGet]
    //    public async Task<IActionResult> GetAllUsers()
    //    {
    //        var users = await _userService.GetAllUsersAsync();
    //        var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
    //        return Ok(userDtos);
    //    }
    //}
}
