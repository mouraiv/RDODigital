// RDODigital.Application/DTOs/StatusConexaoDTO.cs
namespace Application.DTOs;

public class StatusConexaoDTO
{
    public int IdUsuario { get; set; }
    public string? Status { get; set; } // "online", "offline", "instavel"
    public DateTime UltimaVerificacao { get; set; }
    public int ForcaSinal { get; set; } // 0-100
    public string? TipoConexao { get; set; } // "wifi", "dados_moveis", "ethernet", "nenhum"
}

public class AtualizarStatusDTO
{
    public int IdUsuario { get; set; }
    public string? Status { get; set; }
    public int ForcaSinal { get; set; }
    public string? TipoConexao { get; set; }
}