// Application/Interfaces/IRelatorioDiarioService.cs
using Application.DTOs;

namespace Application.Interfaces;

public interface IRelatorioDiarioService
{
    Task<RelatorioDiarioDTO?> GetByIdAsync(int id);
    Task<IEnumerable<RelatorioDiarioDTO>> GetAllAsync();
    Task<RelatorioDiarioDTO> CreateAsync(CreateRelatorioDiarioDTO dto);
    Task<bool> UpdateAsync(UpdateRelatorioDiarioDTO dto);
    Task<bool> DeleteAsync(int id);
}