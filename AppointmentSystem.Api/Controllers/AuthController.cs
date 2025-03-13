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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var loginQuery = new LoginQuery
            {
                UsernameOrEmail = request.Username,
                Password = request.Password
            };

            var result = await _mediator.Send(loginQuery);

            // Check if login was successful or not and return appropriate IActionResult
            if (result.Succeeded)
            {
                // Return the login result as JSON in case of success
                return Ok(result); // Or return Ok(new { token = result.Token }) if you want to return a specific part of the result
            }

            // Return Unauthorized if the login was not successful
            return Unauthorized(new { message = "Invalid username or password." });
        }
    }

    // ✅ DTO for login request
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
