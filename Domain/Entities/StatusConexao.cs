// RDODigital.Domain/Entities/StatusConexao.cs
namespace Domain.Entities;

public class StatusConexao
{
    public int Id_Status { get; set; }
    public int Id_Usuario { get; set; }
    public Usuario? Usuario { get; set; }
    public string? Status { get; set; } // "online", "offline", "instavel"
    public DateTime Ultima_Verificacao { get; set; }
    public int Forca_Sinal { get; set; } // 0-100
    public string? Tipo_Conexao { get; set; } // "wifi", "dados_moveis", "ethernet", "nenhum"
    public decimal Latitude { get; set; }  
    public decimal Longitude { get; set; }
}