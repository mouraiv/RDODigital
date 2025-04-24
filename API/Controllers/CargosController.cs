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
        return Ok(cargos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CargoDTO>> GetById(int id)
    {
        var cargo = await _service.GetByIdAsync(id);
        return Ok(cargo);
    }

    [HttpPost]
    public async Task<ActionResult<CargoDTO>> Create(CreateCargoDTO dto)
    {
        var createdCargo = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdCargo.Id }, new {
            Message = "Cargo criado com sucesso.",
            Cargo = createdCargo
        });
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCargoDTO dto)
    {
        await _service.UpdateAsync(dto);
        return Ok(new { Message = $"Cargo com ID {dto.Id} atualizado com sucesso." });
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok(new { Message = $"Cargo com ID {id} excluído com sucesso." });
    }
}