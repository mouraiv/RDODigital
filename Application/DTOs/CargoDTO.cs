// Application/DTOs/CargoDTO.cs
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
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
        [Required(ErrorMessage = "O campo nome é obrigatório.")]

        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]

        public string? Descricao { get; set; }
    }

    // Application/DTOs/UpdateCargoDTO.cs
    public class UpdateCargoDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        public string? Descricao { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataCriacao { get; set; }
    }
}