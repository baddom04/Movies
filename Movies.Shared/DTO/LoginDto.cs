namespace Movies.Shared.DTO
{
    public class LoginDto
    {
        // The email address the user uses to login
        public required string Email { get; init; }

        // The plain-text password provided by the user for authentication
        public required string Password { get; init; }
    }
}
