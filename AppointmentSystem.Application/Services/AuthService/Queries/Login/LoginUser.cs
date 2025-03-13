// Application/Features/Auth/Queries/Login/LoginQuery.cs
using AppointmentSystem.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AppointmentSystem.Application.Services.AuthService.Queries.Login
{
    // Request (Query)
    public class LoginQuery : IRequest<LoginResult>
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }

    // Handler
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResult>
    {
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResult> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            // Find the user by username or email
            var user = await _userRepository.FindByUsernameAsync(request.UsernameOrEmail);

            if (user == null)
                return LoginResult.Failure("User not found.");

            // Verify the password
            var isValidPassword = await _userRepository.VerifyPasswordAsync(user.UserName, request.Password);
            if (!isValidPassword)
                return LoginResult.Failure("Invalid password.");

            // Generate a token
            var token = await _userRepository.GenerateTokenWithClaimsAsync(user);
            return LoginResult.Success(token);
        }
    }
}