using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IStatusConexaoRepository
    {
        Task AtualizarStatusAsync(StatusConexao status);
        Task<StatusConexao> GetStatusPorUsuarioAsync(int usuarioId);
        
    }
}