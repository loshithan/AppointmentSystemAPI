using AppointmentSystem.Application.DTOs;
using AppointmentSystem.Domain.Entities.AppointmentSystem.Domain.Entities;

public interface IProfessionalAvailabilityRepository : IGenericRepository<ProfessionalAvailability>
{
    Task<List<DoctorWithAvailabilitiesDto>> GetProfessionalsAsync(
    ProfessionalAvailabilityFilterParams filterParams);
}

