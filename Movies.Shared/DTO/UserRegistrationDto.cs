using System.ComponentModel.DataAnnotations;

namespace Movies.Shared.DTO
{
    public class UserRegistrationDto
    {
        // The username chosen by the user; must be unique
        public required string Username { get; init; }

        // User's email address; also used for login
        public required string Email { get; init; }

        // Plain-text password; this will be hashed before saving to the database
        public required string Password { get; init; }

        // Confirm password property
        [Compare("Password")]
        public required string ConfirmPassword { get; set; }
    }
}
