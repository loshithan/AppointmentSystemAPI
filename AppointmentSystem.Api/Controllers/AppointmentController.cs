using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace AppointmentSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private ISender _mediator = null!;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

        [HttpGet]
        public async Task<ActionResult<List<Appointment>>> GetAll()
        {
            var query =  new GetAllAppointments.Query();
            return await  Mediator.Send(query);
        }
    }
}