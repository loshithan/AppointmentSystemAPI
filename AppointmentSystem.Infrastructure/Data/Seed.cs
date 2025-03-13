// Infrastructure/Seeding/SeedData.cs
using AppointmentSystem.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;


namespace AppointmentSystem.Infrastructure.Infrastructure.Seeding
{
    public static class Seed
    {
        public static async Task Initialize(IServiceProvider serviceProvider
        //, IHostEnvironment env
        )
        {
            // if (!env.IsDevelopment())
            // {
            //     return; // Only seed in Development environment
            // }

            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppointmentSystemDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Ensure the database is created and migrated
            await context.Database.MigrateAsync();

            // Seed roles
            string[] roleNames = { "Admin", "Doctor", "Patient" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed Admin User
            var adminUser = new ApplicationUser
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                EmailConfirmed = true
            };

            string adminPassword = "Admin@123";
            var user = await userManager.FindByEmailAsync(adminUser.Email);
            if (user == null)
            {
                var createAdminUser = await userManager.CreateAsync(adminUser, adminPassword);
                if (createAdminUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Seed Doctor User
            var doctorUser = new ApplicationUser
            {
                UserName = "doctor@example.com",
                Email = "doctor@example.com",
                EmailConfirmed = true
            };

            string doctorPassword = "Doctor@123";
            user = await userManager.FindByEmailAsync(doctorUser.Email);
            if (user == null)
            {
                var createDoctorUser = await userManager.CreateAsync(doctorUser, doctorPassword);
                if (createDoctorUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(doctorUser, "Doctor");
                }
            }

            // Seed Patient User
            var patientUser = new ApplicationUser
            {
                UserName = "patient@example.com",
                Email = "patient@example.com",
                EmailConfirmed = true
            };

            string patientPassword = "Patient@123";
            user = await userManager.FindByEmailAsync(patientUser.Email);
            if (user == null)
            {
                var createPatientUser = await userManager.CreateAsync(patientUser, patientPassword);
                if (createPatientUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(patientUser, "Patient");
                }
            }
        }
    }
}