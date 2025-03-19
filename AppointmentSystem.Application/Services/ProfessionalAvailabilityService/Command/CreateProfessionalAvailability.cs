using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Domain.Entities.AppointmentSystem.Domain.Entities;
using MediatR;

namespace AppointmentSystem.Application.Services.ProfessionalAvailabilityService.Command
{
    public class CreateProfessionalAvailability
    {
        public class CreateProfessionalAvailabilityCommand : IRequest<ProfessionalAvailability>
        {
            public Guid Id { get; set; } = Guid.NewGuid();
            public string? ProfessionalId { get; set; } // Foreign Key for Doctor
            public DateTime AvailableDate { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
            public string? HospitalName { get; set; }

            public Specialisation Specialisation { get; set; }

            public string? HospitalAddress { get; set; }
            public string? Price { get; set; }

            public bool IsBooked { get; set; } = false;
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        }
        public class Handler : IRequestHandler<CreateProfessionalAvailabilityCommand, ProfessionalAvailability>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }


            public async Task<ProfessionalAvailability> Handle(CreateProfessionalAvailabilityCommand request, CancellationToken cancellationToken)
            {
                var entity = new ProfessionalAvailability
                {
                    ProfessionalId = request.ProfessionalId,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    AvailableDate = request.AvailableDate,
                    HospitalName = request.HospitalName,
                    Specialisation = request.Specialisation,
                    HospitalAddress = request.HospitalAddress,
                    IsBooked = request.IsBooked,
                    Price = request.Price,
                };

                var newEntity = await _unitOfWork.ProfessionalAvailabilities.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return newEntity;

            }

        }
    }
}