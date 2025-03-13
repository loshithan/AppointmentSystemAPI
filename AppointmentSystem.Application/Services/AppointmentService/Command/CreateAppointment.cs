using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;

namespace AppointmentSystem.Application.Appointments.Commands
{
    public class CreateAppointment : IRequest<Appointment>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? PatientId { get; set; }  // Foreign Key for Patient
        public string? ProfessionalId { get; set; }   // Foreign Key for Doctor
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }

    // public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    // {
    //     public CreateAppointmentCommandValidator()
    //     {
    //         RuleFor(x => x.PatientId).NotEmpty();
    //         RuleFor(x => x.ProfessionalId).NotEmpty();
    //         RuleFor(x => x.AppointmentDate).GreaterThan(DateTime.UtcNow);
    //     }
    // }

    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointment, Appointment>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Appointment> Handle(CreateAppointment request, CancellationToken cancellationToken)
        {
            var appointment = new Appointment
            {
                Id = request.Id,
                PatientId = request.PatientId,
                ProfessionalId = request.ProfessionalId,
                AppointmentDate = request.AppointmentDate,
                Status = request.Status,
                CreatedAt = request.CreatedAt,
                UpdatedAt = request.UpdatedAt
            };

            await _unitOfWork.Appointments.AddAsync(appointment);
            await _unitOfWork.SaveChangesAsync(cancellationToken); // Commit the transaction

            return appointment;
        }
    }
}

