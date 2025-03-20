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
        public Guid? ProfessionalAvailabilityId { get; set; }
    }


    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointment, Appointment>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAppointmentCommandHandler( IUnitOfWork unitOfWork)
        {
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
                UpdatedAt = request.UpdatedAt,
                ProfessionalAvailabilityId = request.ProfessionalAvailabilityId
            };

            await _unitOfWork.Appointments.AddAsync(appointment);
            await _unitOfWork.SaveChangesAsync(cancellationToken); // Commit the transaction

            return appointment;
        }
    }
}

