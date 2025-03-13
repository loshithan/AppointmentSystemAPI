using AppointmentSystem.Application.Services.AuthService.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppointmentSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ✅ CQRS-based login endpoint
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var loginQuery = new LoginQuery
            {
                UsernameOrEmail = request.Username,
                Password = request.Password
            };

            return (IActionResult)await _mediator.Send(loginQuery);
        }
    }

    // ✅ DTO for login request
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
