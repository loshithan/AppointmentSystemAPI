// using Microsoft.AspNetCore.Mvc;
// using AppointmentSystem.Application.Services;
// using AppointmentSystem.Application.DTOs;

// namespace AppointmentSystem.Api.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class AppointmentController : ControllerBase
//     {
//         private readonly IAppointmentService _appointmentService;

//         public AppointmentController(IAppointmentService appointmentService)
//         {
//             _appointmentService = appointmentService;
//         }

//         [HttpGet]
//         public async Task<IActionResult> GetAll()
//         {
//             var appointments = await _appointmentService.GetAllAppointmentsAsync();
//             return Ok(appointments);
//         }
//     }
// }