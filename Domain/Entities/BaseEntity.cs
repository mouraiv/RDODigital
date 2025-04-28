using System.ComponentModel.DataAnnotations;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime Data_criacao { get; set; } = DateTime.UtcNow;
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
}
