using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ProjetoDTO
    {
        public int Id { get; set; }
        public string TituloInfovia { get; set; } = null!;
        public int IdCliente { get; set; }
        public string Cidade { get; set; } = null!;
        public DateTime MesReferencia { get; set; }
        public int IdFiscal { get; set; }
        public int IdSupervisor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string Status { get; set; } = "pendente";
        public decimal ProgressoTempo { get; set; }
        public decimal ProgressoProjeto { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }

    public class CreateProjetoDTO
    {
        [Required(ErrorMessage = "O campo título é obrigatório.")]
        public string TituloInfovia { get; set; } = null!;

        [Required(ErrorMessage = "O campo cliente é obrigatório.")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "O campo cidade é obrigatório.")]
        public string Cidade { get; set; } = null!;

        [Required(ErrorMessage = "O campo mês de referência é obrigatório.")]
        public DateTime MesReferencia { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "O campo fiscal é obrigatório.")]
        public int IdFiscal { get; set; }
        [Required(ErrorMessage = "O campo supervisor é obrigatório.")]
        public int IdSupervisor { get; set; }

        [Required(ErrorMessage = "O campo data de início é obrigatório.")]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "O campo data de fim é obrigatório.")]
        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string Status { get; set; } = "pendente";
        public decimal ProgressoTempo { get; set; }
        public decimal ProgressoProjeto { get; set; }
    }

    public class UpdateProjetoDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo título é obrigatório.")]
        public string TituloInfovia { get; set; } = null!;
        [Required(ErrorMessage = "O campo cliente é obrigatório.")]
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "O campo cidade é obrigatório.")]
        public string Cidade { get; set; } = null!;
        [Required(ErrorMessage = "O campo mês de referência é obrigatório.")]
        public DateTime MesReferencia { get; set; }
        [Required(ErrorMessage = "O campo fiscal é obrigatório.")]
        public int IdFiscal { get; set; }
        [Required(ErrorMessage = "O campo supervisor é obrigatório.")]
        public int IdSupervisor { get; set; }
        [Required(ErrorMessage = "O campo data de início é obrigatório.")]
        public DateTime DataInicio { get; set; }
        [Required(ErrorMessage = "O campo data de fim é obrigatório.")]
        public DateTime DataFim { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string Status { get; set; } = "pendente";
        public decimal ProgressoTempo { get; set; }
        public decimal ProgressoProjeto { get; set; }
    }
}