using AppointmentSystem.Infrastructure.Persistence;
using AppointmentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Domain.Entities.AppointmentSystem.Domain.Entities;

public class ProfessionalAvailabilityRepository : IProfessionalAvailabilityRepository
{
    private readonly AppointmentSystemDbContext _context;

    public ProfessionalAvailabilityRepository(AppointmentSystemDbContext context)
    {
        _context = context;
    }

    public async Task<ProfessionalAvailability> AddAsync(ProfessionalAvailability entity)
    {
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

    public async Task<ProfessionalAvailability> GetByIdAsync(Guid id)
    {
        return await _context.ProfessionalAvailabilities.FindAsync(id);
    }

    public async Task<ProfessionalAvailability> UpdateAsync(ProfessionalAvailability entity)
    {
        _context.ProfessionalAvailabilities.Update(entity);
        return entity;
    }
}