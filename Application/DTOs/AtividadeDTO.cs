// Application/DTOs/AtividadeDTO.cs
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class AtividadeDTO
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string? Item  { get; set; }
        public string? Classe { get; set; }
        public string NomeAtividade { get; set; } = string.Empty;
        public string? UnidadeMedida { get; set; }
    }

    public class CreateAtividadeDTO
    {
        [Required(ErrorMessage = "O campo Id Cliente é obrigatório.")]
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        public string NomeAtividade { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo item é obrigatório.")]
        public string? Item { get; set; }

        [Required(ErrorMessage = "O campo classe é obrigatório.")]
        public string? Classe { get; set; }

        [Required(ErrorMessage = "O campo unidade de medida é obrigatório.")]
        public string? UnidadeMedida { get; set; }
    }
    public class UpdateAtividadeDTO
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O campo Id Cliente é obrigatório.")]
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        public string NomeAtividade { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo item é obrigatório.")]
        public string? Item { get; set; }

        [Required(ErrorMessage = "O campo classe é obrigatório.")]
        public string? Classe { get; set; }

        [Required(ErrorMessage = "O campo unidade de medida é obrigatório.")]
        public string? UnidadeMedida { get; set; }
    }
}

