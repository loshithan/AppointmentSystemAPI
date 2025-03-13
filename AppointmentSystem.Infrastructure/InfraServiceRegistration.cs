using AppointmentSystem.Application.Interfaces.Repositories;
using AppointmentSystem.Infrastructure.Persistence;
using AppointmentSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentSystem.Infrastructure;

public static class InfraServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register DbContext
        services.AddDbContext<AppointmentSystemDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // // Register Identity
        services.AddIdentity<ApplicationUser, IdentityRole>()
       .AddEntityFrameworkStores<AppointmentSystemDbContext>()
       .AddDefaultTokenProviders();

        // Register other infrastructure services
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IProfessionalAvailabilityRepository, ProfessionalAvailabilityRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();



        return services;
    }
}
