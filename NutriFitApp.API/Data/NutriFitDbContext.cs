using Microsoft.EntityFrameworkCore;
using NutriFitApp.Shared.Models;
using System.Collections.Generic;

namespace NutriFitApp.API.Data;

public class NutriFitDbContext : DbContext
{
    public NutriFitDbContext(DbContextOptions<NutriFitDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Dieta> Dietas { get; set; }
    public DbSet<Rutina> Rutinas { get; set; }

    // Puedes configurar relaciones si lo deseas aquí más adelante
}
