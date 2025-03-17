using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Domain.Entities.AppointmentSystem.Domain.Entities;

namespace AppointmentSystem.Application.DTOs
{
    public class DoctorWithAvailabilitiesDto
    {
    public IApplicationUser Doctor { get; set; }
    public List<ProfessionalAvailability> Availabilities { get; set; }
    }
}