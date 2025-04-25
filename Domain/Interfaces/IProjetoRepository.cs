// Infrastructure/Repositories/ProjetoRepository.cs

using Domain.Entities;

namespace Domain.Repositories;

public interface IProjetoRepository
{
    Task<Projeto?> GetByIdAsync(int id);
    Task<IEnumerable<Projeto>> GetAllAsync();
    Task<int> CreateAsync(Projeto Projeto);
    Task<bool> UpdateAsync(Projeto Projeto);
    Task<bool> DeleteAsync(int id);
}