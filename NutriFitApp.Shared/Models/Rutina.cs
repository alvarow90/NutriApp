using System.ComponentModel.DataAnnotations;

namespace NutriFitApp.Shared.Models;

public class Rutina
{
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string Descripcion { get; set; } = string.Empty;

    [Required]
    public int UsuarioId { get; set; }

    // Relación
    public Usuario? Usuario { get; set; }
}
