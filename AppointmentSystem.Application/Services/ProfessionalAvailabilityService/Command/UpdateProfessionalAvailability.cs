using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace AppointmentSystem.Application.Services.ProfessionalAvailabilityService.Command
{
   public class UpdateProfessionalAvailability
{
    public class UpdateProfessionalAvailabilityCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        // public DateTime AvailableDate { get; set; }
        // public TimeSpan StartTime { get; set; }
        // public TimeSpan EndTime { get; set; }
        public bool IsBooked { get; set; }
    }

    public class Handler : IRequestHandler<UpdateProfessionalAvailabilityCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateProfessionalAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var availability = await _unitOfWork.ProfessionalAvailabilities.GetByIdAsync(request.Id);
            if (availability == null) return false;

            // availability.AvailableDate = request.AvailableDate;
            // availability.StartTime = request.StartTime;
            // availability.EndTime = request.EndTime;
            availability.IsBooked = request.IsBooked;
            // availability.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.ProfessionalAvailabilities.UpdateAsync(availability);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

}