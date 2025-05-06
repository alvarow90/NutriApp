using NutriFitApp.Shared.Models;
using NutriFitApp.Shared.DTOs;

namespace NutriFitApp.API.Services.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<Usuario>> GetUsuariosAsync();
    Task<Usuario?> GetUsuarioByIdAsync(int id);
    Task<Usuario> CreateUsuarioAsync(Usuario usuario);
    Task<bool> UpdateUsuarioAsync(Usuario usuario); // ✅ Ya está agregado correctamente
    Task<bool> DeleteUsuarioAsync(int id);
}
