using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IStatusConexaoRepository
    {
        Task<StatusConexao> GetByIdAsync(int id);
        Task<IEnumerable<StatusConexao>> GetByUsuarioIdAsync(int usuarioId);
        Task<int> CreateAsync(StatusConexao status);
        Task<bool> UpdateAsync(StatusConexao status);
        Task<bool> DeleteAsync(int id);
        
    }
}