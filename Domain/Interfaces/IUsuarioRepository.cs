using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByIdAsync(int id);
        Task<Usuario?> GetByIdEmailAsync(string email);
        Task<Usuario?> GetByMatriculaAsync(int matricula);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(int id, Usuario usuario);
        Task DeleteAsync(int id);
    }
}