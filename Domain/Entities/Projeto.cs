using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Projeto
{
    [Key]
    public int Id_projeto { get; set; }
    public string? Titulo_infovia { get; set; }
    public int Id_cliente { get; set; }
    public string? Cidade { get; set; }
    public DateTime Mes_referencia { get; set; }
    public int Id_fiscal { get; set; }
    public int Id_supervisor { get; set; }
    public DateTime Data_inicio { get; set; }
    public DateTime Data_fim { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string Status { get; set; } = "pendente";
    public decimal Progresso_tempo { get; set; }
    public decimal Progresso_projeto { get; set; }
    public DateTime Data_criacao { get; set; } = DateTime.Now;
}
