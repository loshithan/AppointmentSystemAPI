namespace AppointmentSystem.Domain.Entities
{
    namespace AppointmentSystem.Domain.Entities
    {
        public class ProfessionalAvailability
        {
            public Guid Id { get; set; } = Guid.NewGuid();
            public string ProfessionalId { get; set; } // Foreign Key for Doctor
            public DateTime AvailableDate { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
            public bool IsBooked { get; set; } = false;

            public string? HospitalName { get; set; }
            public string? HospitalAddress { get; set; }
            public string? Price { get; set; }
            public Specialisation? Specialisation { get; set; } = null;
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        }
        public enum Specialisation
        {
            Cardiology,
            Neurology,
            Orthopedics,
            Pediatrics,
            Dermatology,
            Oncology,
            Gynecology,
            Urology,
            Ophthalmology,
            EmergencyMedicine,
            GeneralSurgery,
            Radiology,
            Psychiatry,
            Endocrinology,
            Gastroenterology,
            Pulmonology,
            Nephrology,
            Hematology,
            Rheumatology,
            InfectiousDiseases
        }
    }

}
