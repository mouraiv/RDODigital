
namespace Domain.Entities;

public class Atividade : BaseEntity
{
    public int? Id_cliente { get; set; }
    public Cliente? Cliente { get; set; } 
    public string? Item { get; set; }
    public string? Classe { get; set; }
    public string? Nome_atividade { get; set; }
    public string? Unidade_medida { get; set; }
}