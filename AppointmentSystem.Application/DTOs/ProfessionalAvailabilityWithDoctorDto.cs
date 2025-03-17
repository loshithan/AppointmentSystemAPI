using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Domain.Entities.AppointmentSystem.Domain.Entities;

namespace AppointmentSystem.Application.DTOs
{
    public class ProfessionalAvailabilityWithDoctorDto
    {
        public ProfessionalAvailability Availability { get; set; }
        public IApplicationUser Doctor { get; set; }

    }
}