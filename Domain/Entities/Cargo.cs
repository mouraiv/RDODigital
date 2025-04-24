// Domain/Entities/Cargo.cs
namespace Domain.Entities;

public class Cargo
{
    public int Id_cargo { get; set; }
    public string Nome_cargo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTime Data_criacao { get; set; } = DateTime.UtcNow;
}