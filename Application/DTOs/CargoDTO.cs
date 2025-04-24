// Application/DTOs/CargoDTO.cs
namespace Application.DTOs;

public class CargoDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTime DataCriacao { get; set; }
}

// Application/DTOs/CreateCargoDTO.cs
public class CreateCargoDTO
{
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
}

// Application/DTOs/UpdateCargoDTO.cs
public class UpdateCargoDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
}