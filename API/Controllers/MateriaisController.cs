using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MateriaisController : ControllerBase
{
    private readonly IMaterialService _service;

    public MateriaisController(IMaterialService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MaterialDTO>>> GetAll()
    {
        var Materiais = await _service.GetAllAsync();
        return Ok(Materiais);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MaterialDTO>> GetById(int id)
    {
        var Material = await _service.GetByIdAsync(id);
        return Ok(Material);
    }

    [HttpPost]
    public async Task<ActionResult<MaterialDTO>> Create(CreateMaterialDTO dto)
    {
        var createdMaterial = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdMaterial.Id }, new {
            Message = "Material criado com sucesso.",
            Material = createdMaterial
        });
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateMaterialDTO dto)
    {
        await _service.UpdateAsync(dto);
        return Ok(new { Message = $"Material com ID {dto.Id} atualizado com sucesso." });
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok(new { Message = $"Material com ID {id} excluído com sucesso." });
    }
}