using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace AppointmentSystem.Application.Services.AppointmentService.Query
{
    public class GetAppointmentById
    {
        public class Query : IRequest<Appointment>
        {
            public Guid Id { get; set; }
        }
    
        public class Handler : IRequestHandler<Query, Appointment>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
          

            public async Task<Appointment> Handle(Query request, CancellationToken cancellationToken)
            {
                var appointment = await _unitOfWork.Appointments.GetByIdAsync(request.Id);
                return appointment ?? throw new KeyNotFoundException("Appointment not found");
            }
        }
    }
}
