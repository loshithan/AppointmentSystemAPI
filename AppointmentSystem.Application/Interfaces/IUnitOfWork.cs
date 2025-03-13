public interface IUnitOfWork
    {
        IAppointmentRepository Appointments { get; }
        IProfessionalAvailabilityRepository ProfessionalAvailabilities { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }