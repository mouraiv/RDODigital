// Application/Interfaces/ICargoService.cs
using Application.DTOs;

namespace Application.Interfaces;

public interface ICargoService
{
    Task<CargoDTO?> GetByIdAsync(int id);
    Task<IEnumerable<CargoDTO>> GetAllAsync();
    Task<CargoDTO> CreateAsync(CreateCargoDTO dto);
    Task<bool> UpdateAsync(UpdateCargoDTO dto);
    Task<bool> DeleteAsync(int id);
}