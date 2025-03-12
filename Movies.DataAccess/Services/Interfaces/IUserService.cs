using Movies.DataAccess.Models;

namespace Movies.DataAccess.Services.Interfaces
{
    public interface IUserService
    {
        // Registers a new user.
        Task<User> RegisterUserAsync(UserRegistrationDto registrationDto);

        // Logs in a user.
        Task<User> LoginUserAsync(LoginDto loginDto);

        // Retrieves a user by their ID.
        Task<User> GetUserByIdAsync(int userId);

        // Updates a user's profile information.
        Task UpdateUserProfileAsync(int userId, UserProfileUpdateDto profileDto);

        // Gets all users (optional, e.g., for admin use).
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
