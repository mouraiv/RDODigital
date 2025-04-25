using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class RelatorioDiarioDTO
    {
        public int Id { get; set; }
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

    public class CreateRelatorioDiarioDTO
    {
        [Required(ErrorMessage = "O campo Id projeto é obrigatório.")]
        public int Id_projeto { get; set; }
        [Required(ErrorMessage = "O campo Id usuário é obrigatório.")]
        public int Id_usuario { get; set; }
        [Required(ErrorMessage = "O campo Data/hora é obrigatório.")]
        [DataType(DataType.Date)]
        public DateTime Data_hora { get; set; }
        [Required(ErrorMessage = "O campo Id atividade é obrigatório.")]
        public int Id_atividade { get; set; }
        [Required(ErrorMessage = "O campo Quantidade é obrigatório.")]
        public decimal Quantidade { get; set; }
    }

    public class UpdateRelatorioDiarioDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Id projeto é obrigatório.")]
        public int Id_projeto { get; set; }
        [Required(ErrorMessage = "O campo Id usuário é obrigatório.")]
        public int Id_usuario { get; set; }
        [Required(ErrorMessage = "O campo Data/hora é obrigatório.")]
        [DataType(DataType.Date)]
        public DateTime Data_hora { get; set; }
        [Required(ErrorMessage = "O campo Id atividade é obrigatório.")]
        public int Id_atividade { get; set; }
        [Required(ErrorMessage = "O campo Quantidade é obrigatório.")]
        public decimal Quantidade { get; set; }
    }
}