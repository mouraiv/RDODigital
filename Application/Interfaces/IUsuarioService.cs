using Application.DTOs;

namespace Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> GetByIdAsync(int id);
        Task<IEnumerable<UsuarioDTO>> GetAllAsync();
        Task<UsuarioDTO> AddAsync(CreateUsuarioDTO usuarioDto);
        Task UpdateAsync(int id, UpdateUsuarioDTO usuarioDto);
        Task DeleteAsync(int id);
        Task UpdatePhotoPathAsync(int usuarioId, string pathFile);
    }
}