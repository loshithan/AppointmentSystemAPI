using MediatR;

public class GetAllAppointments
{
    public class Query : IRequest<List<Appointment>>
    {
        public string? SearchParam { get; set; } 
        public string? PatientId{ get; set; }
        public bool? isAdmin { get; set; }
        //= "" ; // Foreign Key for Patient
       
        
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
            if (request.isAdmin == true)
            {
                var (appointments, _) = await _unitOfWork.Appointments.GetAllAsync(request.SearchParam);
                return appointments;
            }
            else
            {
                var (appointments, _) = await _unitOfWork.Appointments.GetAppointmentsByPatientIdAsync(request.PatientId);
                return appointments;
            }
          
        }
    }
}