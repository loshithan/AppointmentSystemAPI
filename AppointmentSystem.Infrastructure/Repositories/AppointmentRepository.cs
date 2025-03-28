using AppointmentSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppointmentSystemDbContext _context;

    public AppointmentRepository(AppointmentSystemDbContext context)
    {
        _context = context;
    }

    public async Task<Appointment> AddAsync(Appointment entity)
    {
        await _context.Appointments.AddAsync(entity);
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
            return false;

        _context.Appointments.Remove(appointment);
        var professionalAvailability = await _context.ProfessionalAvailabilities
            .FindAsync(appointment.ProfessionalAvailabilityId);

        if (professionalAvailability != null)
        {
            professionalAvailability.IsBooked = false;
            // Modify the professionalAvailability entity as needed here
            _context.ProfessionalAvailabilities.Update(professionalAvailability);
        }
        return true;
    }

    public async Task<(List<Appointment>, int)> GetAllAsync(string parameters)
    {
        IQueryable<Appointment> query = _context.Appointments.Include(a => a.ProfessionalAvailability);

        //Apply raw SQL query if parameters are provided (use with caution!)
        if (!string.IsNullOrEmpty(parameters))
        {
            query = _context.Appointments.Where(a => a.PatientId == parameters);
        }

        // Execute the query and get the results
        var appointments = await query.ToListAsync();

        // Return the list of appointments and the count
        return (appointments, appointments.Count);
    }

    public async Task<(List<Appointment>, int)> GetAppointmentsByPatientIdAsync(string parameters)
    {
         var appointments = await _context.Appointments
        .Include(a => a.ProfessionalAvailability) // Include related data if needed
        .Where(a => a.PatientId == parameters)
        .ToListAsync();
        
        return (appointments, appointments.Count);

    }

    public async Task<Appointment> GetByIdAsync(Guid id)
    {
        return await _context.Appointments.FindAsync(id);
    }

    public async Task<Appointment> UpdateAsync(Appointment entity)
    {
        _context.Appointments.Update(entity);
        return entity;
    }
}
