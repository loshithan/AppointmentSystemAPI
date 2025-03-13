using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace AppointmentSystem.Application.Services.AppointmentService.Command
{
    public class DeleteAppointment : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointment, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAppointmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteAppointment request, CancellationToken cancellationToken)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(request.Id);
            if (appointment == null)
                return false;

            var deleted = await _unitOfWork.Appointments.DeleteAsync(request.Id);
            if (!deleted)
                return false;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
