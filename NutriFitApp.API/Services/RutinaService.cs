using Microsoft.EntityFrameworkCore;
using NutriFitApp.API.Data;
using NutriFitApp.API.Services.Interfaces;
using NutriFitApp.Shared.Models;

namespace NutriFitApp.API.Services;

public class RutinaService : IRutinaService
{
    private readonly NutriFitDbContext _context;

    public RutinaService(NutriFitDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Rutina>> GetAllAsync()
    {
        return await _context.Rutinas.ToListAsync();
    }

    public async Task<Rutina?> GetByIdAsync(int id)
    {
        return await _context.Rutinas.FindAsync(id);
    }

    public async Task<Rutina> CreateAsync(Rutina rutina)
    {
        _context.Rutinas.Add(rutina);
        await _context.SaveChangesAsync();
        return rutina;
    }

    public async Task<bool> UpdateAsync(Rutina rutina)
    {
        _context.Rutinas.Update(rutina);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var rutina = await _context.Rutinas.FindAsync(id);
        if (rutina == null) return false;

        _context.Rutinas.Remove(rutina);
        return await _context.SaveChangesAsync() > 0;
    }
}
