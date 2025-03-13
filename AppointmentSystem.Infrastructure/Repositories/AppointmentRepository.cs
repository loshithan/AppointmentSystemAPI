
using AppointmentSystem.Infrastructure.Persistence;

public class AppointmentRepository : IAppointmentRepository
{
     private readonly AppointmentSystemDbContext _context;
    //  List<Appointment> appointments = new List<Appointment>
    // {
    //     new Appointment
    //     {
    //         Id = Guid.NewGuid(),
    //         PatientId = "P001",
    //         ProfessionalId = "D001",
    //         AppointmentDate = DateTime.UtcNow.AddDays(1),
    //         Status = AppointmentStatus.Confirmed,
    //         CreatedAt = DateTime.UtcNow
    //     },
    //     new Appointment
    //     {
    //         Id = Guid.NewGuid(),
    //         PatientId = "P002",
    //         ProfessionalId = "D002",
    //         AppointmentDate = DateTime.UtcNow.AddDays(2),
    //         Status = AppointmentStatus.Pending,
    //         CreatedAt = DateTime.UtcNow
    //     },
    //     new Appointment
    //     {
    //         Id = Guid.NewGuid(),
    //         PatientId = "P003",
    //         ProfessionalId = "D001",
    //         AppointmentDate = DateTime.UtcNow.AddDays(3),
    //         Status = AppointmentStatus.Canceled,
    //         CreatedAt = DateTime.UtcNow
    //     }
    // };


    public AppointmentRepository(AppointmentSystemDbContext context)
    {
        _context = context;
    }
    public Task<Appointment> AddAsync(Appointment entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<(List<Appointment>, int)> GetAllAsync(string parameters)
    {
        var appointments = _context.Appointments.ToList();
    return Task.FromResult((appointments, appointments.Count));

    }

    public Task<Appointment> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Appointment> UpdateAsync(Appointment entity)
    {
        throw new NotImplementedException();
    }
}