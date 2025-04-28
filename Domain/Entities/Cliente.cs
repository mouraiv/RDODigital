
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Cliente : BaseEntity
    {
        public string? Nome_cliente { get; set; }
        public bool Ativo { get; set; }
        public string? Foto_perfil { get; set; }
        
    }
}