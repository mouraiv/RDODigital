using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RelatoriosDiariosController : ControllerBase
{
    private readonly IRelatorioDiarioService _service;

    public RelatoriosDiariosController(IRelatorioDiarioService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RelatorioDiarioDTO>>> GetAll()
    {
        var RelatoriosDiarios = await _service.GetAllAsync();
        return Ok(RelatoriosDiarios);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RelatorioDiarioDTO>> GetById(int id)
    {
        var RelatorioDiario = await _service.GetByIdAsync(id);
        return Ok(RelatorioDiario);
    }

    [HttpPost]
    public async Task<ActionResult<RelatorioDiarioDTO>> Create(CreateRelatorioDiarioDTO dto)
    {
        var createdRelatorioDiario = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdRelatorioDiario.Id }, new {
            Message = "Relatorio Diario criado com sucesso.",
            RelatorioDiario = createdRelatorioDiario
        });
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateRelatorioDiarioDTO dto)
    {
        await _service.UpdateAsync(dto);
        return Ok(new { Message = $"Relatorio Diario com ID {dto.Id} atualizado com sucesso." });
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok(new { Message = $"Relatorio Diario com ID {id} excluído com sucesso." });
    }
}