

using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class RelatorioDiario
{
    [Key]
    public int Id_relatorio { get; set; }
    public int Id_projeto { get; set; }
    public int Id_usuario { get; set; }
    public DateTime Data_hora { get; set; }
    public int Id_atividade { get; set; }
    public decimal Quantidade { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public DateTime? Ultima_sincronizacao { get; set; } 
    public bool Sincronizado { get; set; }
}
