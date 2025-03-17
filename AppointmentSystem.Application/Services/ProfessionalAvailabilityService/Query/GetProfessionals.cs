using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Application.DTOs;
using AppointmentSystem.Domain.Entities.AppointmentSystem.Domain.Entities;
using MediatR;

namespace AppointmentSystem.Application.Services.ProfessionalAvailabilityService.Query
{
    public class GetProfessionals
    {
        public class GetProfessionalsQuery : IRequest<List<DoctorWithAvailabilitiesDto>>
        {
            public string? HospitalName { get; set; }
            public string? DoctorName { get; set; }

            public Specialisation? Specialisation { get; set; }
            public DateTime? AvailableDate { get; set; }




        }

        public class Handler : IRequestHandler<GetProfessionalsQuery,List<DoctorWithAvailabilitiesDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<List<DoctorWithAvailabilitiesDto>> Handle(GetProfessionalsQuery request, CancellationToken cancellationToken)
            {
                ProfessionalAvailabilityFilterParams professionalAvailabilityFilterParams = new ProfessionalAvailabilityFilterParams()
                {
                    AvailableDate = request.AvailableDate,
                    HospitalName = request.HospitalName,
                    DoctorName = request.DoctorName,
                    Specialisation = request.Specialisation
                };
                List<DoctorWithAvailabilitiesDto> result = await _unitOfWork.ProfessionalAvailabilities.GetProfessionalsAsync(professionalAvailabilityFilterParams);
                return result;
            }
        }
    }
}