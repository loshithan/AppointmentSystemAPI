using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace AppointmentSystem.Application.Services.AppointmentService.Command
{
    public class DeleteAppointmentValidator: AbstractValidator<DeleteAppointment>
    {
        public DeleteAppointmentValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Appointment ID is required.")
                .NotEqual(Guid.Empty).WithMessage("Invalid appointment ID.");
        }
    }
}