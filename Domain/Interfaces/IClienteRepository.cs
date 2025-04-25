using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente?> GetByIdAsync(int id);
        Task<Cliente?> GetByIdClienteAsync(string nome_cliente);
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task AddAsync(Cliente cliente);
        Task UpdateAsync(int id, Cliente cliente);
        Task DeleteAsync(int id);
    }
}