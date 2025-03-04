using AppointmentSystem.Domain.Entities.AppointmentSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser, IApplicationUser
{
    public string Role { get; set; } = "Patient";
    //  public ICollection<Appointment> PatientAppointments { get; set; } // For Patients
    //     public ICollection<Appointment> ProfessionalAppointments { get; set; } // For Doctors
    //     public ICollection<ProfessionalAvailability> Availabilities { get; set; } // For Doctors
}