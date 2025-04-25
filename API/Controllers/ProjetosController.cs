using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjetosController : ControllerBase
{
    private readonly IProjetoService _service;

    public ProjetosController(IProjetoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjetoDTO>>> GetAll()
    {
        var Projetos = await _service.GetAllAsync();
        return Ok(Projetos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjetoDTO>> GetById(int id)
    {
        var Projeto = await _service.GetByIdAsync(id);
        return Ok(Projeto);
    }

    [HttpPost]
    public async Task<ActionResult<ProjetoDTO>> Create(CreateProjetoDTO dto)
    {
        var createdProjeto = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdProjeto.Id }, new {
            Message = "Projeto criado com sucesso.",
            Projeto = createdProjeto
        });
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateProjetoDTO dto)
    {
        await _service.UpdateAsync(dto);
        return Ok(new { Message = $"Projeto com ID {dto.Id} atualizado com sucesso." });
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok(new { Message = $"Projeto com ID {id} excluído com sucesso." });
    }
}