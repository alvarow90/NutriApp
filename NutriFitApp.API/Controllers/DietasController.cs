using Microsoft.AspNetCore.Mvc;
using NutriFitApp.API.Services.Interfaces;
using NutriFitApp.Shared.Models;

namespace NutriFitApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DietasController : ControllerBase
{
    private readonly IDietaService _dietaService;

    public DietasController(IDietaService dietaService)
    {
        _dietaService = dietaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var dietas = await _dietaService.GetAllAsync();
        return Ok(dietas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var dieta = await _dietaService.GetByIdAsync(id);
        if (dieta == null) return NotFound();
        return Ok(dieta);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Dieta dieta)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var nueva = await _dietaService.CreateAsync(dieta);
        return CreatedAtAction(nameof(GetById), new { id = nueva.Id }, nueva);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Dieta dieta)
    {
        if (id != dieta.Id)
            return BadRequest("ID mismatch");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var actualizado = await _dietaService.UpdateAsync(dieta);
        if (!actualizado) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _dietaService.DeleteAsync(id);
        if (!eliminado) return NotFound();

        return NoContent();
    }
}
