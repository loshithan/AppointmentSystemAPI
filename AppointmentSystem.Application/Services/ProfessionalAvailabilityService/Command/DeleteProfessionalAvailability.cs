using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace AppointmentSystem.Application.Services.ProfessionalAvailabilityService.Command
{
    public class DeleteProfessionalAvailability
{
    public class DeleteProfessionalAvailabilityCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<DeleteProfessionalAvailabilityCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteProfessionalAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ProfessionalAvailabilities.DeleteAsync(request.Id);
            if (!result) return false;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

}