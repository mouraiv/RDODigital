// Infrastructure/Repositories/AtividadeRepository.cs

using Domain.Entities;

namespace Domain.Repositories;

public interface IAtividadeRepository
{
    Task<Atividade?> GetByIdAsync(int id);
    Task<IEnumerable<Atividade>> GetAllAsync();
    Task<Atividade?> GetByNameAsync(string nome);
    Task<int> CreateAsync(Atividade atividade);
    Task<bool> UpdateAsync(Atividade atividade);
    Task<bool> DeleteAsync(int id);
}