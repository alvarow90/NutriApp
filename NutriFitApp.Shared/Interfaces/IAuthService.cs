using NutriFitApp.Shared.DTOs;
using NutriFitApp.Shared.Models;

namespace NutriFitApp.Shared.Interfaces;

public interface IAuthService
{
    Task<bool> LoginAsync(LoginDTO loginDto);
    Task<bool> RegisterAsync(Usuario usuario);
    string? GetToken();
    void Logout();
}
