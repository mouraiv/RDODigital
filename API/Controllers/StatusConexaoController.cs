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
        if (status == null)
        {
            return NotFound(new { Message = $"Status de conexão com ID {id} não encontrado." });
        }
        return Ok(status);
    }

    [HttpGet("usuario/{usuarioId}")]
    public async Task<ActionResult<IEnumerable<StatusConexaoDTO>>> GetByUsuarioId(int usuarioId)
    {
        var statusList = await _service.GetByUsuarioIdAsync(usuarioId);
        if (statusList == null || !statusList.Any())
        {
            return NotFound(new { Message = $"Nenhum status encontrado para o usuário ID {usuarioId}." });
        }
        return Ok(statusList);
    }

    [HttpPost]
    public async Task<ActionResult<StatusConexaoDTO>> Create(CreateStatusConexaoDTO dto)
    {

        var createdStatus = await _service.CreateAsync(dto);
        if (createdStatus == null)
        {
            return BadRequest(new { Message = "Erro ao criar o status de conexão." });
        }
        return CreatedAtAction(nameof(GetById), new { id = createdStatus.IdStatus }, new{
            Message = "Status de conexão criado com sucesso.",
            StatusConexao = createdStatus
        });
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateStatusConexaoDTO dto)
    {
        var result = await _service.UpdateAsync(dto);
        if (!result)
        {
            return NotFound(new { Message = $"Status de conexão com ID {dto.IdStatus} não encontrado para atualização." });
        }
        return Ok(new { Message = "Status de conexão atualizado com sucesso." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result)
        {
            return NotFound(new { Message = $"Status de conexão com ID {id} não encontrado para exclusão." });
        }
        return Ok(new { Message = "Status de conexão excluído com sucesso." });
    }
}