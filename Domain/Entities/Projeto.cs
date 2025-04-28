using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Projeto : BaseEntity
{
    public string? Titulo_infovia { get; set; }
    public int Id_cliente { get; set; }
    public string? Cidade { get; set; }
    public DateTime Mes_referencia { get; set; }
    public int Id_fiscal { get; set; }
    public int Id_supervisor { get; set; }
    public DateTime Data_inicio { get; set; }
    public DateTime Data_fim { get; set; }
    public string Status { get; set; } = "pendente";
    public decimal Progresso_tempo { get; set; }
    public decimal Progresso_projeto { get; set; }
}
