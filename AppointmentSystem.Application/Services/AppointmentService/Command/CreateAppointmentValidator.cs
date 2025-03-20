using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Application.Appointments.Commands;
using FluentValidation;

namespace AppointmentSystem.Application.Services.AppointmentService.Command
{
    public class CreateAppointmentValidator: AbstractValidator<CreateAppointment>
    {
        public CreateAppointmentValidator()
        {
            RuleFor(x => x.PatientId)
                .NotEmpty().WithMessage("Patient ID is required.");

            RuleFor(x => x.ProfessionalId)
                .NotEmpty().WithMessage("Professional ID is required.");

            RuleFor(x => x.AppointmentDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Appointment date must be in the future.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid appointment status.");
        }
    }
}