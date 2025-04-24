using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CargosController : ControllerBase
{
    private readonly ICargoService _service;
    private readonly ILogger<CargosController> _logger;

    public CargosController(ICargoService service, ILogger<CargosController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CargoDTO>>> GetAll()
    {
        var cargos = await _service.GetAllAsync();
        if (cargos == null || !cargos.Any())
        {
            return NotFound(new { Message = "Nenhum cargo encontrado." });
        }
        return Ok(cargos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CargoDTO>> GetById(int id)
    {
        var cargo = await _service.GetByIdAsync(id);
        if (cargo == null)
        {
            return NotFound(new { Message = $"Cargo com ID {id} não encontrado." });
        }
        return Ok(cargo);
    }

    [HttpPost]
    public async Task<ActionResult<CargoDTO>> Create(CreateCargoDTO dto)
    {
        var createdCargo = await _service.CreateAsync(dto);
        if (createdCargo == null)
        {
            return BadRequest(new { Message = "Erro ao criar o cargo." });
        }
        return CreatedAtAction(nameof(GetById), new { id = createdCargo.Id }, createdCargo);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCargoDTO dto)
    {
        var result = await _service.UpdateAsync(dto);
        if (!result)
        {
            return NotFound(new { Message = $"Cargo com ID {dto.Id} não encontrado para atualização." });
        }
        return NoContent();
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result)
        {
            return NotFound(new { Message = $"Cargo com ID {id} não encontrado para exclusão." });
        }
        return NoContent();
    }
}