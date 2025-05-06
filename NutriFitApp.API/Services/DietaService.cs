using Microsoft.EntityFrameworkCore;
using NutriFitApp.API.Data;
using NutriFitApp.API.Services.Interfaces;
using NutriFitApp.Shared.Models;

namespace NutriFitApp.API.Services;

public class DietaService : IDietaService
{
    private readonly NutriFitDbContext _context;

    public DietaService(NutriFitDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Dieta>> GetAllAsync()
    {
        return await _context.Dietas.ToListAsync();
    }

    public async Task<Dieta?> GetByIdAsync(int id)
    {
        return await _context.Dietas.FindAsync(id);
    }

    public async Task<Dieta> CreateAsync(Dieta dieta)
    {
        _context.Dietas.Add(dieta);
        await _context.SaveChangesAsync();
        return dieta;
    }

    public async Task<bool> UpdateAsync(Dieta dieta)
    {
        _context.Dietas.Update(dieta);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var dieta = await _context.Dietas.FindAsync(id);
        if (dieta == null) return false;

        _context.Dietas.Remove(dieta);
        return await _context.SaveChangesAsync() > 0;
    }
}
