// Application/Interfaces/IStatusConexaoService.cs
using Application.DTOs;

namespace Application.Interfaces;

public interface IStatusConexaoService
{
    Task<StatusConexaoDTO?> GetByIdAsync(int id);
    Task<IEnumerable<StatusConexaoDTO>> GetByUsuarioIdAsync(int usuarioId);
    Task<StatusConexaoDTO> CreateAsync(CreateStatusConexaoDTO dto);
    Task<bool> UpdateAsync(UpdateStatusConexaoDTO dto);
    Task<bool> DeleteAsync(int id);
}