using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Domain.Entities.AppointmentSystem.Domain.Entities;
using MediatR;

namespace AppointmentSystem.Application.Services.ProfessionalAvailabilityService.Query
{
    public class GetAllProfessionalAvailabilities
{
    public class GetAllProfessionalAvailabilitiesQuery : IRequest<List<ProfessionalAvailability>> 
    {
       public string? SearchParam { get; set; }

    }

    public class Handler : IRequestHandler<GetAllProfessionalAvailabilitiesQuery, List<ProfessionalAvailability>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProfessionalAvailability>> Handle(GetAllProfessionalAvailabilitiesQuery request, CancellationToken cancellationToken)
        {
            var (availabilities, _) = await _unitOfWork.ProfessionalAvailabilities.GetAllAsync(request.SearchParam);
            return availabilities;
        }
    }
}

}