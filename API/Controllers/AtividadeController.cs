using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AtividadesController : ControllerBase
{
    private readonly IAtividadeService _service;

    public AtividadesController(IAtividadeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AtividadeDTO>>> GetAll()
    {
        var Atividades = await _service.GetAllAsync();
        return Ok(Atividades);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AtividadeDTO>> GetById(int id)
    {
        var Atividade = await _service.GetByIdAsync(id);
        return Ok(Atividade);
    }

    [HttpPost]
    public async Task<ActionResult<AtividadeDTO>> Create(CreateAtividadeDTO dto)
    {
        var createdAtividade = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdAtividade.Id }, new {
            Message = "Atividade criado com sucesso.",
            Atividade = createdAtividade
        });
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateAtividadeDTO dto)
    {
        await _service.UpdateAsync(dto);
        return Ok(new { Message = $"Atividade com ID {dto.Id} atualizado com sucesso." });
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok(new { Message = $"Atividade com ID {id} excluído com sucesso." });
    }
}