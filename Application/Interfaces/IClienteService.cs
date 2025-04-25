using Application.DTOs;

namespace Application.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteDTO> GetByIdAsync(int id);
        Task<IEnumerable<ClienteDTO>> GetAllAsync();
        Task<ClienteDTO> AddAsync(CreateClienteDTO ClienteDto, Stream fileStream, string fileName);
        Task UpdateAsync(int id, UpdateClienteDTO ClienteDto, Stream fileStream, string fileName);
        Task DeleteAsync(int id);
        Task UpdatePhotoPathAsync(int ClienteId, string pathFile);
    }
}