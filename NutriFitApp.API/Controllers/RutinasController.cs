using Microsoft.AspNetCore.Mvc;
using NutriFitApp.API.Services.Interfaces;
using NutriFitApp.Shared.Models;

namespace NutriFitApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RutinasController : ControllerBase
{
    private readonly IRutinaService _rutinaService;

    public RutinasController(IRutinaService rutinaService)
    {
        _rutinaService = rutinaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var rutinas = await _rutinaService.GetAllAsync();
        return Ok(rutinas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var rutina = await _rutinaService.GetByIdAsync(id);
        if (rutina == null) return NotFound();
        return Ok(rutina);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Rutina rutina)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var nueva = await _rutinaService.CreateAsync(rutina);
        return CreatedAtAction(nameof(GetById), new { id = nueva.Id }, nueva);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Rutina rutina)
    {
        if (id != rutina.Id)
            return BadRequest("ID mismatch");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var actualizado = await _rutinaService.UpdateAsync(rutina);
        if (!actualizado) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _rutinaService.DeleteAsync(id);
        if (!eliminado) return NotFound();

        return NoContent();
    }
}
