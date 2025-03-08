using MediatR;

public class GetAllAppointments
{
    public class Query : IRequest<List<Appointment>>
    {
        public string SearchParam { get; set; } = "" ; // Foreign Key for Patient
       
        
    }

    public class Handler : IRequestHandler<Query, List<Appointment>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Appointment>> Handle(Query request, CancellationToken cancellationToken)
        {
            var (appointments, _) = await _unitOfWork.Appointments.GetAllAsync(request.SearchParam);
            return appointments;
        }
    }
}