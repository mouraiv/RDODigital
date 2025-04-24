// Infrastructure/Repositories/CargoRepository.cs

using Domain.Entities;

namespace Domain.Repositories;

public interface ICargoRepository
{
    Task<Cargo> GetByIdAsync(int id);
    Task<IEnumerable<Cargo>> GetAllAsync();
    Task<Cargo?> GetByNameAsync(string nome);
    Task<int> CreateAsync(Cargo cargo);
    Task<bool> UpdateAsync(Cargo cargo);
    Task<bool> DeleteAsync(int id);
}