// RDODigital.Domain/Entities/StatusConexao.cs
namespace Domain.Entities;

public class StatusConexao
{
    public int IdStatus { get; set; }
    public int IdUsuario { get; set; }
    public Usuario? Usuario { get; set; }
    public string? Status { get; set; } // "online", "offline", "instavel"
    public DateTime UltimaVerificacao { get; set; }
    public int ForcaSinal { get; set; } // 0-100
    public string? TipoConexao { get; set; } // "wifi", "dados_moveis", "ethernet", "nenhum"
}