using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Movies.DataAccess.Exceptions;
using Movies.DataAccess.Models;
using Movies.DataAccess.Services.Interfaces;
using Movies.Shared.DTO;

namespace Movies.DataAccess.Services
{
    public class UserService(UserManager<User> userManager,
                       SignInManager<User> signInManager) : IUserService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;

        // Registers a new user.
        public async Task<User> RegisterUserAsync(UserRegistrationDto registrationDto)
        {
            var user = new User
            {
                UserName = registrationDto.Username,
                Email = registrationDto.Email,
                RegistrationDate = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, registrationDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new Exception($"User registration failed: {errors}");
            }

            return user;
        }

        // Logs in a user.
        public async Task<User> LoginUserAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email) ?? throw new EntityNotFoundException("User not found");

            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            // Optionally, generate and return a JWT token here if needed.
            return user;
        }

        // Retrieves a user by their ID.
        public async Task<User> GetUserByIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var user = await _userManager.FindByIdAsync(userId);
            return user ?? throw new EntityNotFoundException("User not found");
        }

        // Updates a user's profile information.
        public async Task UpdateUserProfileAsync(string userId, UserProfileUpdateDto profileDto)
        {
            var user = await _userManager.FindByIdAsync(userId) 
                ?? throw new EntityNotFoundException("User not found");

            // Update the user's properties.
            user.UserName = profileDto.Username;
            user.Email = profileDto.Email;
            user.Bio = profileDto.Bio;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new Exception($"User update failed: {errors}");
            }
        }

        // Gets all users (e.g., for administrative purposes).
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
    }
}
