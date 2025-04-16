namespace Application.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public int? Matricula { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Cargo { get; set; }
        public string? FotoPerfil { get; set; }
        public string? TelefoneCorporativo { get; set; }
        public DateTime DataAdmissao { get; set; }
    }

    public class CreateUsuarioDTO
    {
        public int? Matricula { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? Cargo { get; set; }
        public string? TelefoneCorporativo { get; set; }
        public DateTime DataAdmissao { get; set; }
        public bool Ativo { get; set; }
    }

    public class UpdateUsuarioDTO
    {
        public int? Matricula { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Cargo { get; set; }
        public string? TelefoneCorporativo { get; set; }
        public DateTime DataAdmissao { get; set; }
        public bool Ativo { get; set; }
    }
}