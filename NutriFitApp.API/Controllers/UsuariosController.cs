using Microsoft.AspNetCore.Mvc;
using NutriFitApp.API.Services.Interfaces;
using NutriFitApp.Shared.Models;

namespace NutriFitApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    // GET: api/Usuarios
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
    {
        var usuarios = await _usuarioService.GetUsuariosAsync();
        return Ok(usuarios);
    }

    // GET: api/Usuarios/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> GetUsuario(int id)
    {
        var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
        if (usuario == null)
            return NotFound();

        return Ok(usuario);
    }

    // POST: api/Usuarios
    [HttpPost]
    public async Task<ActionResult<Usuario>> CreateUsuario([FromBody] Usuario usuario)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var nuevoUsuario = await _usuarioService.CreateUsuarioAsync(usuario);
        return CreatedAtAction(nameof(GetUsuario), new { id = nuevoUsuario.Id }, nuevoUsuario);
    }

    // PUT: api/Usuarios/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUsuario(int id, [FromBody] Usuario usuario)
    {
        if (id != usuario.Id)
            return BadRequest("El ID no coincide.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var actualizado = await _usuarioService.UpdateUsuarioAsync(usuario);
        if (!actualizado)
            return NotFound();

        return NoContent();
    }

    // DELETE: api/Usuarios/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var eliminado = await _usuarioService.DeleteUsuarioAsync(id);
        if (!eliminado)
            return NotFound();

        return NoContent();
    }
}
