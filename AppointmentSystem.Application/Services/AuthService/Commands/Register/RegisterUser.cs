// Application/Features/Auth/Commands/Register/RegisterCommand.cs
using AppointmentSystem.Application.Interfaces.Repositories;
using MediatR;

namespace AppointmentSystem.Application.Features.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<RegistrationResult>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegistrationResult>
    {
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<RegistrationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.RegisterAsync(request.Username, request.Email, request.Password);
        }
    }
}