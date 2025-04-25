// Infrastructure/Repositories/MaterialRepository.cs

using Domain.Entities;

namespace Domain.Repositories;

public interface IMaterialRepository
{
    Task<Material?> GetByIdAsync(int id);
    Task<IEnumerable<Material>> GetAllAsync();
    Task<Material?> GetByNameAsync(string nome);
    Task<int> CreateAsync(Material Material);
    Task<bool> UpdateAsync(Material Material);
    Task<bool> DeleteAsync(int id);
}