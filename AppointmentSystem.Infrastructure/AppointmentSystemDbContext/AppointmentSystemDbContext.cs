using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppointmentSystem.Domain.Entities.AppointmentSystem.Domain.Entities;

namespace AppointmentSystem.Infrastructure.Persistence
{
    public class AppointmentSystemDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppointmentSystemDbContext(DbContextOptions<AppointmentSystemDbContext> options)
            : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<ProfessionalAvailability> ProfessionalAvailabilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Remove navigation properties in Appointment: Only foreign keys are stored
            modelBuilder.Entity<Appointment>()
                .Property(a => a.PatientId)
                .IsRequired();

            modelBuilder.Entity<Appointment>()
                .Property(a => a.ProfessionalId)
                .IsRequired();

            // Remove navigation properties in ProfessionalAvailability: Only foreign keys are stored
            modelBuilder.Entity<ProfessionalAvailability>()
                .Property(pa => pa.ProfessionalId)
                .IsRequired();

            // Define relationships for Appointment
            modelBuilder.Entity<Appointment>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(a => a.ProfessionalId)
                .OnDelete(DeleteBehavior.Restrict);

            // Define relationships for ProfessionalAvailability
            modelBuilder.Entity<ProfessionalAvailability>()
                .HasOne<Professional>()
                .WithMany()
                .HasForeignKey(pa => pa.ProfessionalId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
