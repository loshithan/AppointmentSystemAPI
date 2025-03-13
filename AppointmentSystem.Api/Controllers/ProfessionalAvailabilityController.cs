using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using AppointmentSystem.Application.Services.ProfessionalAvailabilityService.Query;
using AppointmentSystem.Application.Services.ProfessionalAvailabilityService.Command;
using AppointmentSystem.Domain.Entities.AppointmentSystem.Domain.Entities;

namespace AppointmentSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize] // Requires authentication for all actions in this controller
    public class ProfessionalAvailabilityController : ControllerBase
    {
        private ISender _mediator = null!;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

        [HttpGet]
        public async Task<ActionResult<List<ProfessionalAvailability>>> GetAll()
        {
            var query = new GetAllProfessionalAvailabilities.GetAllProfessionalAvailabilitiesQuery();
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfessionalAvailability>> GetById(Guid id)
        {
            var query = new GetProfessionalAvailabilityById.GetProfessionalAvailabilityByIdQuery { Id = id };
            var result = await Mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProfessionalAvailability>> Create([FromBody] CreateProfessionalAvailability.CreateProfessionalAvailabilityCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProfessionalAvailability.UpdateProfessionalAvailabilityCommand command)
        {
            if (id != command.Id) return BadRequest("Mismatched ID");
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteProfessionalAvailability.DeleteProfessionalAvailabilityCommand { Id = id });
            return NoContent();
        }
    }
}
