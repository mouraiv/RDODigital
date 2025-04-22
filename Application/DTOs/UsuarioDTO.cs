using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public int? Matricula { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Cargo { get; set; }
        public string? Foto_perfil { get; set; }
        public string? TelefoneCorporativo { get; set; }
        public DateTime DataAdmissao { get; set; }
    }

    public class CreateUsuarioDTO
    {
        [Required(ErrorMessage = "O campo matrícula é obrigatório.")]
        public int? Matricula { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email em formato inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatória")]
        [DataType(DataType.Password)]
        public string? Senha { get; set; }

        [Required(ErrorMessage = "O campo cargo é obrigatória")]
        public string? Cargo { get; set; }

        public string? Foto_perfil { get; set; }

        [Required(ErrorMessage = "O campo telefone corporativo é obrigatória")]
        [Phone(ErrorMessage = "Número de telefone inválido")]
        public string? TelefoneCorporativo { get; set; }

        [Required(ErrorMessage = "O campo data de admissão é obrigatória")]
        [DataType(DataType.Date)]
        public DateTime DataAdmissao { get; set; }

        public bool Ativo { get; set; }
    }

    public class UpdateUsuarioDTO
    {
        [Required(ErrorMessage = "O campo matrícula é obrigatório.")]
        public int? Matricula { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email em formato inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatória")]
        [DataType(DataType.Password)]
        public string? Senha { get; set; }

        [Required(ErrorMessage = "O campo cargo é obrigatória")]
        public string? Cargo { get; set; }

        public string? Foto_perfil { get; set; }

        [Required(ErrorMessage = "O campo telefone corporativo é obrigatória")]
        [Phone(ErrorMessage = "Número de telefone inválido")]
        public string? TelefoneCorporativo { get; set; }

        [Required(ErrorMessage = "O campo data de admissão é obrigatória")]
        [DataType(DataType.Date)]
        public DateTime DataAdmissao { get; set; }

        public bool Ativo { get; set; }
    }
}
