using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class StatusConexaoController : ControllerBase
{
    private readonly IStatusConexaoService _statusService;

    public StatusConexaoController(IStatusConexaoService statusService)
    {
        _statusService = statusService;
    }

    [HttpPost("atualizar")]
    public async Task<IActionResult> AtualizarStatus([FromBody] AtualizarStatusDTO statusDto)
    {
        await _statusService.AtualizarStatusAsync(statusDto);
        return Ok(new { message = "Status atualizado com sucesso" });
    }

    [HttpGet("usuario/{idUsuario}")]
    public async Task<ActionResult<StatusConexaoDTO>> GetStatusUsuario(int idUsuario)
    {
        var status = await _statusService.GetStatusUsuarioAsync(idUsuario);
        return Ok(status ?? new StatusConexaoDTO { 
            IdUsuario = idUsuario,
            Status = "offline",
            TipoConexao = "nenhum",
            ForcaSinal = 0
        });
    }
}