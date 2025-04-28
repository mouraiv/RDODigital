namespace Domain.Entities;

public class RelatorioDiario : RegistroCampo
{
    public int Id_usuario { get; set; }
    public DateTime? Ultima_sincronizacao { get; set; } 
    public bool Sincronizado { get; set; }
}
