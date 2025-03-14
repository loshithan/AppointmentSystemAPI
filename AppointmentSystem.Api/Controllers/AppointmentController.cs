using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using AppointmentSystem.Application.Appointments.Commands;
using AppointmentSystem.Application.Services.AppointmentService.Query;
using AppointmentSystem.Application.Services.AppointmentService.Command;

namespace AppointmentSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize] // Requires authentication for all actions in this controller
    public class AppointmentController : ControllerBase
    {
        private ISender _mediator = null!;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

        [HttpGet]
        public async Task<ActionResult<List<Appointment>>> GetAll([FromQuery] string searchParam = "")
        {
            var query = new GetAllAppointments.Query() { SearchParam = searchParam };
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetById(Guid id)
        {
            var query = new GetAppointmentById.Query { Id = id };
            var result = await Mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Appointment>> Create([FromBody] CreateAppointment command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAppointment command)
        {
            if (id != command.Id) return BadRequest("Mismatched appointment ID");
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteAppointment { Id = id });
            return NoContent();
        }
    }
}
