// Application/Interfaces/IAtividadeService.cs
using Application.DTOs;

namespace Application.Interfaces;

public interface IAtividadeService
{
    Task<AtividadeDTO?> GetByIdAsync(int id);
    Task<IEnumerable<AtividadeDTO>> GetAllAsync();
    Task<AtividadeDTO> CreateAsync(CreateAtividadeDTO dto);
    Task<bool> UpdateAsync(UpdateAtividadeDTO dto);
    Task<bool> DeleteAsync(int id);
}