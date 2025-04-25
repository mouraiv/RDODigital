// Application/DTOs/CargoDTO.cs
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class MaterialDTO
    {
        public int Id { get; set; }
        public int IdProjeto { get; set; }
        public DateTime DataHora { get; set; }
        public int IdAtividade { get; set; }
        public decimal Quantidade { get; set; }
    }

    // Application/DTOs/CreateMaterialDTO.cs
    public class CreateMaterialDTO
    {
        [Required(ErrorMessage = "O campo Id do projeto é obrigatório.")]
        public int IdProjeto { get; set; }
        [Required(ErrorMessage = "O campo Data e Hora é obrigatório.")]
        [DataType(DataType.Date)]
        public DateTime DataHora { get; set; }
        [Required(ErrorMessage = "O campo Id da atividade é obrigatório.")]
        public int IdAtividade { get; set; }
        [Required(ErrorMessage = "O campo Quantidade é obrigatório.")]
        public decimal Quantidade { get; set; }
    }
    // Application/DTOs/UpdateMaterialDTO.cs
    public class UpdateMaterialDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Id do projeto é obrigatório.")]
        public int IdProjeto { get; set; }
        [Required(ErrorMessage = "O campo Data e Hora é obrigatório.")]
        [DataType(DataType.Date)]
        public DateTime DataHora { get; set; }
        [Required(ErrorMessage = "O campo Id da atividade é obrigatório.")]
        public int IdAtividade { get; set; }
        [Required(ErrorMessage = "O campo Quantidade é obrigatório.")]
        public decimal Quantidade { get; set; }
    }
}