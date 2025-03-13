
using AppointmentSystem.Infrastructure.Persistence;

public class ProfessionalAvailabilityRepository : IProfessionalAvailabilityRepository
{
     private readonly AppointmentSystemDbContext _context;

    public ProfessionalAvailabilityRepository(AppointmentSystemDbContext context)
    {
        _context = context;
    }
    public Task<AppointmentSystem.Domain.Entities.AppointmentSystem.Domain.Entities.ProfessionalAvailability> AddAsync(AppointmentSystem.Domain.Entities.AppointmentSystem.Domain.Entities.ProfessionalAvailability entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<(List<AppointmentSystem.Domain.Entities.AppointmentSystem.Domain.Entities.ProfessionalAvailability>, int)> GetAllAsync(string parameters)
    {
        throw new NotImplementedException();
    }

    public Task<AppointmentSystem.Domain.Entities.AppointmentSystem.Domain.Entities.ProfessionalAvailability> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<AppointmentSystem.Domain.Entities.AppointmentSystem.Domain.Entities.ProfessionalAvailability> UpdateAsync(AppointmentSystem.Domain.Entities.AppointmentSystem.Domain.Entities.ProfessionalAvailability entity)
    {
        throw new NotImplementedException();
    }
}