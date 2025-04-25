// Domain/Entities/Cargo.cs
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Atividade
{
    [Key]
    public int Id_atividade { get; set; }
    public int? Id_cliente { get; set; }
    public Cliente? Cliente { get; set; } 
    public string? Item { get; set; }
    public string? Classe { get; set; }
    public string? Nome_atividade { get; set; }
    public string? Unidade_medida { get; set; }
}