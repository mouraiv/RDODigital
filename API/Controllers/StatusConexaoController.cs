using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatusConexaoController : ControllerBase
{
    private readonly IStatusConexaoService _service;
    private readonly ILogger<StatusConexaoController> _logger;

    public StatusConexaoController(
        IStatusConexaoService service,
        ILogger<StatusConexaoController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StatusConexaoDTO>> GetById(int id)
    {
        var status = await _service.GetByIdAsync(id);
        return Ok(status);
    }

    [HttpGet("usuario/{usuarioId}")]
    public async Task<ActionResult<IEnumerable<StatusConexaoDTO>>> GetByUsuarioId(int usuarioId)
    {
        var statusList = await _service.GetByUsuarioIdAsync(usuarioId);
        return Ok(statusList);
    }

    [HttpPost]
    public async Task<ActionResult<StatusConexaoDTO>> Create(CreateStatusConexaoDTO dto)
    {

        var createdStatus = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdStatus.IdStatus }, new{
            Message = "Status de conexão criado com sucesso.",
            StatusConexao = createdStatus
        });
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateStatusConexaoDTO dto)
    {
        var result = await _service.UpdateAsync(dto);
        return Ok(new { Message = "Status de conexão atualizado com sucesso." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        return Ok(new { Message = "Status de conexão excluído com sucesso." });
    }
}