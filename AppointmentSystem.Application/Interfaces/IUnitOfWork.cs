public interface IUnitOfWork
    {
        IAppointmentRepository Appointments { get; }
        IProfessionalAvailabilityRepository professionalAvailabilities { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }