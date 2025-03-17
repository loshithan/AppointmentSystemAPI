using System.ComponentModel.DataAnnotations.Schema;
using AppointmentSystem.Domain.Entities.AppointmentSystem.Domain.Entities;

public class Appointment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string PatientId { get; set; }  // Foreign Key for Patient
    public string ProfessionalId { get; set; }   // Foreign Key for Doctor
    public DateTime AppointmentDate { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    // Foreign Key for ProfessionalAvailability
    public Guid? ProfessionalAvailabilityId { get; set; }

    // Navigation property for ProfessionalAvailability
    [ForeignKey("ProfessionalAvailabilityId")]
    public ProfessionalAvailability ProfessionalAvailability { get; set; }


}
public enum AppointmentStatus
{
    Pending,
    Confirmed,
    Canceled
}