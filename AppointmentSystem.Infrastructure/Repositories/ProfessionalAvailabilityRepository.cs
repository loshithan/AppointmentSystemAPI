using AppointmentSystem.Infrastructure.Persistence;
using AppointmentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Domain.Entities.AppointmentSystem.Domain.Entities;
using AppointmentSystem.Application.DTOs;

public class ProfessionalAvailabilityRepository : IProfessionalAvailabilityRepository
{
    private readonly AppointmentSystemDbContext _context;

    public ProfessionalAvailabilityRepository(AppointmentSystemDbContext context)
    {
        _context = context;
    }

    public async Task<ProfessionalAvailability> AddAsync(ProfessionalAvailability entity)
    {// Check if there is an existing availability for the same professional on the same date and within the same time slot
        var existingAvailability = await _context.ProfessionalAvailabilities
            .Where(pa => pa.ProfessionalId == entity.ProfessionalId &&
                         pa.AvailableDate.Date == entity.AvailableDate.Date &&
                         ((pa.StartTime <= entity.StartTime && pa.EndTime > entity.StartTime) || // Overlapping start time
                          (pa.StartTime < entity.EndTime && pa.EndTime >= entity.EndTime) ||     // Overlapping end time
                          (pa.StartTime >= entity.StartTime && pa.EndTime <= entity.EndTime)))    // Completely within the new slot
            .FirstOrDefaultAsync();

        if (existingAvailability != null)
        {
            throw new InvalidOperationException("A session already exists for the same professional on the same date and within the specified time slot.");
        }
        await _context.ProfessionalAvailabilities.AddAsync(entity);
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.ProfessionalAvailabilities.FindAsync(id);
        if (entity == null)
            return false;

        _context.ProfessionalAvailabilities.Remove(entity);
        return true;
    }

    public async Task<(List<ProfessionalAvailability>, int)> GetAllAsync(string parameters = null)
    {
        IQueryable<ProfessionalAvailability> query = _context.ProfessionalAvailabilities;

        if (!string.IsNullOrEmpty(parameters))
        {
            query = _context.ProfessionalAvailabilities.FromSqlRaw(parameters);
        }

        var availabilityList = await query.ToListAsync();
        return (availabilityList, availabilityList.Count);
    }
    // Overloaded method for fetching availability by Professional ID
    public async Task<(List<ProfessionalAvailability>, int)> GetAllByProfessionalIdAsync(string professionalId)
    {
        IQueryable<ProfessionalAvailability> query = _context.ProfessionalAvailabilities
            .Where(pa => pa.ProfessionalId == professionalId); // Filter by Professional ID

        var availabilityList = await query.ToListAsync();
        return (availabilityList, availabilityList.Count);
    }

    public async Task<ProfessionalAvailability> GetByIdAsync(Guid id)
    {
        return await _context.ProfessionalAvailabilities.FindAsync(id);
    }

    public async Task<List<DoctorWithAvailabilitiesDto>> GetProfessionalsAsync(
    ProfessionalAvailabilityFilterParams filterParams)
    {
        var query = from pa in _context.ProfessionalAvailabilities
                    join doc in _context.Users on pa.ProfessionalId equals doc.Id
                    join userRole in _context.UserRoles on doc.Id equals userRole.UserId
                    where (!filterParams.AvailableDate.HasValue || pa.AvailableDate.Date == filterParams.AvailableDate.Value.Date) &&
                          (string.IsNullOrEmpty(filterParams.HospitalName) || pa.HospitalName.Contains(filterParams.HospitalName)) &&
                          (string.IsNullOrEmpty(filterParams.DoctorName) || doc.UserName.Contains(filterParams.DoctorName)) &&
                          (!filterParams.Specialisation.HasValue || pa.Specialisation == filterParams.Specialisation.Value) &&
                          (userRole.RoleId == "54caf907-14c0-4458-8890-a8231168a613" || userRole.RoleId == "507544d4-b31f-4efb-ae8a-3a09d76293de" ) // Filter by role ID
                    select new ProfessionalAvailabilityWithDoctorDto
                    {
                        Availability = pa,
                        Doctor = doc
                    };

        var groupedResults = await query
       .GroupBy(x => x.Doctor)
       .Select(g => new DoctorWithAvailabilitiesDto
       {
           Doctor = g.Key,
           Availabilities = g.Select(x => x.Availability).ToList()
       })
       .ToListAsync();

        return groupedResults;
    }

    public async Task<ProfessionalAvailability> UpdateAsync(ProfessionalAvailability entity)
    {
        _context.ProfessionalAvailabilities.Update(entity);
        return entity;
    }
}