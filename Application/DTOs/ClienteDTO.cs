using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string? NomeCliente { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public string? Foto_perfil { get; set; }
    }

    public class CreateClienteDTO
    {
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        public string? NomeCliente { get; set; }

        public bool Ativo { get; set; }
    }

    public class UpdateClienteDTO
    {
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        public string? NomeCliente { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataCadastro { get; set; }

        public string? Foto_perfil { get; set; }

        public bool Ativo { get; set; }
    }
}