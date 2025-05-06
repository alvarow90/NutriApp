using NutriFitApp.Shared.Models;

namespace NutriFitApp.API.Services.Interfaces;

public interface IDietaService
{
    Task<IEnumerable<Dieta>> GetAllAsync();
    Task<Dieta?> GetByIdAsync(int id);
    Task<Dieta> CreateAsync(Dieta dieta);
    Task<bool> UpdateAsync(Dieta dieta);
    Task<bool> DeleteAsync(int id);
}
