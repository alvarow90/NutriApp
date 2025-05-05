using NutriFitApp.Shared.Enums;

namespace NutriFitApp.Shared.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public TipoUsuario Rol { get; set; }

    // Relaciones
    public int? NutriologoId { get; set; }
    public int? EntrenadorId { get; set; }
}
