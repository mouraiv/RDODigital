using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Cargo
{
    [Key]
    public int Id_cargo { get; set; }
    public string? Nome_cargo { get; set; } 
    public string? Descricao { get; set; }
    public DateTime Data_criacao { get; set; } = DateTime.UtcNow;
}