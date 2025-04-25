using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Material
{
    [Key]
    public int Id_material { get; set; }
    public int Id_projeto { get; set; }
    public DateTime Data_hora { get; set; }
    public int Id_atividade { get; set; }
    public decimal Quantidade { get; set; }
}
