// Application/Interfaces/IProjetoService.cs
using Application.DTOs;

namespace Application.Interfaces;

public interface IProjetoService
{
    Task<ProjetoDTO?> GetByIdAsync(int id);
    Task<IEnumerable<ProjetoDTO>> GetAllAsync();
    Task<ProjetoDTO> CreateAsync(CreateProjetoDTO dto);
    Task<bool> UpdateAsync(UpdateProjetoDTO dto);
    Task<bool> DeleteAsync(int id);
}