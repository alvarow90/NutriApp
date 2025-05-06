using System.ComponentModel.DataAnnotations;
using NutriFitApp.Shared.Enums;

namespace NutriFitApp.Shared.Models;

public class Usuario
{
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public TipoUsuario Rol { get; set; }

    public int? NutriologoId { get; set; }
    public int? EntrenadorId { get; set; }
}
