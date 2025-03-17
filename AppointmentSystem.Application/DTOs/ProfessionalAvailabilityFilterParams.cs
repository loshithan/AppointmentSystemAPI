using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Domain.Entities.AppointmentSystem.Domain.Entities;

namespace AppointmentSystem.Application.DTOs
{
    public class ProfessionalAvailabilityFilterParams
    {
    public DateTime? AvailableDate { get; set; } = null;
    public string? HospitalName { get; set; } = string.Empty;
    public string? DoctorName { get; set; } = string.Empty;
    public Specialisation? Specialisation { get; set; } = null;
    }
}