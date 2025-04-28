// Application/DTOs/StatusConexaoDTO.cs
namespace Application.DTOs
{
    public class StatusConexaoDTO
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string? Status { get; set; }
        public DateTime UltimaVerificacao { get; set; }
        public int ForcaSinal { get; set; }
        public string? TipoConexao { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }

    // Application/DTOs/CreateStatusConexaoDTO.cs
    public class CreateStatusConexaoDTO
    {
        public string? Status { get; set; }
        public int ForcaSinal { get; set; }
        public string? TipoConexao { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }

    // Application/DTOs/UpdateStatusConexaoDTO.cs
    public class UpdateStatusConexaoDTO
    {
        public int Id { get; set; }
        public string? Status { get; set; }
        public int ForcaSinal { get; set; }
        public string? TipoConexao { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}