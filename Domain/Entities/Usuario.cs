using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public int? Matricula { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? SenhaHash { get; set; }
        public string? Cargo { get; set; }
        public string? FotoPerfil { get; set; }
        public string? TelefoneCorporativo { get; set; }
        public DateTime DataAdmissao { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}