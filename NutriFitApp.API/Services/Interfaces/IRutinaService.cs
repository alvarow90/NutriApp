using NutriFitApp.Shared.Models;

namespace NutriFitApp.API.Services.Interfaces;

public interface IRutinaService
{
    Task<IEnumerable<Rutina>> GetAllAsync();
    Task<Rutina?> GetByIdAsync(int id);
    Task<Rutina> CreateAsync(Rutina rutina);
    Task<bool> UpdateAsync(Rutina rutina);
    Task<bool> DeleteAsync(int id);
}
