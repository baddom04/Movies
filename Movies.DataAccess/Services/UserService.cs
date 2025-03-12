using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Models;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO;

namespace Movies.DataAccess.Services
{
    public class UserService(MoviesDbContext context) : IUserService
    {
        private readonly MoviesDbContext _context = context;

        public async Task<User> RegisterUserAsync(UserRegistrationDto registrationDto)
        {
            // Create a new user and map properties from the DTO.
            var user = new User
            {
                Username = registrationDto.Username,
                Email = registrationDto.Email,
                // Use a proper hashing mechanism in production
                PasswordHash = HashPassword(registrationDto.Password),
                RegistrationDate = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> LoginUserAsync(LoginDto loginDto)
        {
            // Find the user by email.
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            return user;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId) ?? throw new EntityNotFoundException();
        }

        public async Task UpdateUserProfileAsync(int userId, UserProfileUpdateDto profileDto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            // Update properties based on the DTO.
            user.Username = profileDto.Username;
            user.Email = profileDto.Email;
            user.Bio = profileDto.Bio;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Dummy method for password hashing.
        private string HashPassword(string password)
        {
            // Replace with an actual hashing algorithm, e.g., BCrypt.
            return password; // For demonstration only.
        }

        // Dummy method for password verification.
        private bool VerifyPassword(string password, string storedHash)
        {
            // Replace with an actual verification mechanism.
            return password == storedHash; // For demonstration only.
        }
    }
}
