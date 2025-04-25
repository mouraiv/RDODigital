
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Cliente
    {
        [Key]
        public int Id_cliente { get; set; }
        public string? Nome_cliente { get; set; }
        public DateTime Data_cadastro { get; set; } = DateTime.UtcNow;
        public bool Ativo { get; set; }
        public string? Foto_perfil { get; set; }
        
    }
}