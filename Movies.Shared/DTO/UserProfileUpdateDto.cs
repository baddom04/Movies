namespace Movies.Shared.DTO
{
    public class UserProfileUpdateDto
    {
        // Updated username; if allowed to change
        public string? Username { get; init; }

        // Updated email address
        public string? Email { get; init; }

        // Updated bio or description
        public string? Bio { get; init; }
    }
}
