using AppointmentSystem.Infrastructure.Persistence;

public class UnitOfWork:IUnitOfWork
{
    private readonly AppointmentSystemDbContext _context;
    private IAppointmentRepository _appointments;
    private IProfessionalAvailabilityRepository _professionalAvailabilities;

    public UnitOfWork(AppointmentSystemDbContext context)
    {
        _context = context;
    }

    public IAppointmentRepository Appointments => _appointments ??= new AppointmentRepository(_context);

    public IProfessionalAvailabilityRepository ProfessionalAvailabilities => _professionalAvailabilities ??= new ProfessionalAvailabilityRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
