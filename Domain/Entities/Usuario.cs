using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Usuario
    {
        [Key]
        public int Id_usuario { get; set; }
        public int? Matricula { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha_hash { get; set; }
        public string? Cargo { get; set; } 
        public string? Foto_perfil { get; set; }
        public string? Telefone_corporativo { get; set; }
        public DateTime Data_admissao { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }

        public Usuario() {}

        public Usuario(string senha)
        {
            ValidarSenha(senha);
        }

        public void ValidarSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new DomainException("A senha não pode ser vazia");

                var regex = new Regex(@"^(?=.*[A-Z])(?=.*[!@#$%^&*])(?=.*[0-9])(?=.*[a-z]).{8,}$");

                if (!regex.IsMatch(senha))
                {
                    throw new DomainException(
                        "A senha deve conter:\n" +
                        "- No mínimo 8 caracteres\n" +
                        "- Pelo menos 1 letra maiúscula\n" +
                        "- Pelo menos 1 letra minúscula\n" +
                        "- Pelo menos 1 número\n" +
                        "- Pelo menos 1 caractere especial (!@#$%^&*)");
                }

            Senha_hash = BCrypt.Net.BCrypt.HashPassword(senha);
        }

    }
            
}