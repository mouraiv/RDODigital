namespace Domain.Entities;

public abstract class RegistroCampo : BaseEntity
{
    public int Id_projeto { get; set; }
    public int Id_atividade { get; set; }
    public DateTime Data_hora { get; set; }
    public decimal Quantidade { get; set; }
}
