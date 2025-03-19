namespace AppointmentSystem.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        // Find a user by username or email
        Task<IApplicationUser> FindByUsernameAsync(string username);

        // Verify a user's password
        Task<bool> VerifyPasswordAsync(string username, string password);

        // Register a new user
        Task<RegistrationResult> RegisterAsync(string username, string email, string password,string role);

        // Generate a token for a user
        Task<string> GenerateTokenWithClaimsAsync(IApplicationUser user);
    }
}
