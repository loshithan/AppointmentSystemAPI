using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Domain.Entities.AppointmentSystem.Domain.Entities;
using MediatR;

namespace AppointmentSystem.Application.Services.ProfessionalAvailabilityService.Query
{
    public class GetProfessionalAvailabilityById
{
    public class GetProfessionalAvailabilityByIdQuery : IRequest<ProfessionalAvailability>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<GetProfessionalAvailabilityByIdQuery, ProfessionalAvailability>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProfessionalAvailability> Handle(GetProfessionalAvailabilityByIdQuery request, CancellationToken cancellationToken)
        {
            var availability = await _unitOfWork.ProfessionalAvailabilities.GetByIdAsync(request.Id);
            return availability ?? throw new KeyNotFoundException("Professional availability not found");
        }
    }
}

}