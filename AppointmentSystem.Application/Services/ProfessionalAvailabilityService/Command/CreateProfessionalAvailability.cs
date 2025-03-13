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
                    EndTime = request.EndTime
                };

                var newEntity = await _unitOfWork.ProfessionalAvailabilities.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return newEntity;

            }

        }
    }
}