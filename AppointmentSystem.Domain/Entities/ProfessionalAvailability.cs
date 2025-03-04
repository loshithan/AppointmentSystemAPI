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
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

}
