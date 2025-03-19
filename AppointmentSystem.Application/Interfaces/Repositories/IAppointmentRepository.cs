 public interface IAppointmentRepository : IGenericRepository<Appointment>
{
     Task<(List<Appointment>, int)> GetAppointmentsByPatientIdAsync(string parameters);
}