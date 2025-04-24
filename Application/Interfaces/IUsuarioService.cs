using Application.DTOs;

namespace Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> GetByIdAsync(int id);
        Task<IEnumerable<UsuarioDTO>> GetAllAsync();
        Task<UsuarioDTO> AddAsync(CreateUsuarioDTO usuarioDto, Stream fileStream, string fileName);
        Task UpdateAsync(int id, UpdateUsuarioDTO usuarioDto, Stream fileStream, string fileName);
        Task DeleteAsync(int id);
        Task UpdatePhotoPathAsync(int usuarioId, string pathFile);
    }
}