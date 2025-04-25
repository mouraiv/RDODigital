using Domain.Entities;

namespace Domain.Repositories;

public interface IRelatorioDiarioRepository
{
    Task<RelatorioDiario?> GetByIdAsync(int id);
    Task<IEnumerable<RelatorioDiario>> GetAllAsync();
    Task<int> CreateAsync(RelatorioDiario RelatorioDiario);
    Task<bool> UpdateAsync(RelatorioDiario RelatorioDiario);
    Task<bool> DeleteAsync(int id);
}