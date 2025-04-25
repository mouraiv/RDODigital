using Application.DTOs;

namespace Application.Interfaces;

public interface IMaterialService
{
    Task<MaterialDTO?> GetByIdAsync(int id);
    Task<IEnumerable<MaterialDTO>> GetAllAsync();
    Task<MaterialDTO> CreateAsync(CreateMaterialDTO dto);
    Task<bool> UpdateAsync(UpdateMaterialDTO dto);
    Task<bool> DeleteAsync(int id);
}