using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace AppointmentSystem.Application.Services.AppointmentService.Command
{
    public class UpdateAppointment : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string? PatientId { get; set; }  // Foreign Key for Patient
        public string? ProfessionalId { get; set; }   // Foreign Key for Doctor
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointment, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAppointmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateAppointment request, CancellationToken cancellationToken)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(request.Id);
            if (appointment == null)
                return false;

            appointment.PatientId = request.PatientId;
            appointment.ProfessionalId = request.ProfessionalId;
            appointment.AppointmentDate = request.AppointmentDate;
            appointment.Status = request.Status;
            appointment.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Appointments.UpdateAsync(appointment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
