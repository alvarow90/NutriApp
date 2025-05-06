using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriFitApp.API.Services.Interfaces;
using NutriFitApp.Shared.Enums;
using NutriFitApp.Shared.Models;

namespace NutriFitApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrador")]
public class AdminController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public AdminController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    // GET: api/admin/usuarios
    [HttpGet("usuarios")]
    public async Task<IActionResult> GetAllUsuarios()
    {
        var usuarios = await _usuarioService.GetUsuariosAsync();
        return Ok(usuarios);
    }

    // POST: api/admin/crear-nutriologo
    [HttpPost("crear-nutriologo")]
    public async Task<IActionResult> CrearNutriologo([FromBody] Usuario usuario)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        usuario.Rol = TipoUsuario.Nutriologo;
        var creado = await _usuarioService.CreateUsuarioAsync(usuario);
        return CreatedAtAction(nameof(GetAllUsuarios), new { id = creado.Id }, creado);
    }

    // POST: api/admin/crear-entrenador
    [HttpPost("crear-entrenador")]
    public async Task<IActionResult> CrearEntrenador([FromBody] Usuario usuario)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        usuario.Rol = TipoUsuario.Entrenador;
        var creado = await _usuarioService.CreateUsuarioAsync(usuario);
        return CreatedAtAction(nameof(GetAllUsuarios), new { id = creado.Id }, creado);
    }

    // DELETE: api/admin/usuario/5
    [HttpDelete("usuario/{id}")]
    public async Task<IActionResult> EliminarUsuario(int id)
    {
        var eliminado = await _usuarioService.DeleteUsuarioAsync(id);
        if (!eliminado)
            return NotFound();

        return NoContent();
    }
}
