using Application.DTOs;

namespace Application.Interfaces
{
    public interface IStatusConexaoService
    {
        Task AtualizarStatusAsync(AtualizarStatusDTO statusDto);
        Task<StatusConexaoDTO> GetStatusUsuarioAsync(int idUsuario);
    }
}