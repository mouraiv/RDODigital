// RDODigital.Infra/Repositories/StatusConexaoRepository.cs
using Domain.Entities;
using Domain.Interfaces;
using Dapper;
using Infra.Data;

namespace Infra.Repositories;

public class StatusConexaoRepository : IStatusConexaoRepository
{
    private readonly DapperContext _context;
    
    public StatusConexaoRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task AtualizarStatusAsync(StatusConexao status)
    {
        var query = @"INSERT INTO StatusConexao 
                     (id_usuario, status, ultima_verificacao, forca_sinal, tipo_conexao)
                     VALUES (@IdUsuario, @Status, @UltimaVerificacao, @ForcaSinal, @TipoConexao)
                     ON DUPLICATE KEY UPDATE
                     status = VALUES(status),
                     ultima_verificacao = VALUES(ultima_verificacao),
                     forca_sinal = VALUES(forca_sinal),
                     tipo_conexao = VALUES(tipo_conexao)";
        
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new {
            IdUsuario = status.IdUsuario,
            Status = status.Status,
            UltimaVerificacao = status.UltimaVerificacao,
            ForcaSinal = status.ForcaSinal,
            TipoConexao = status.TipoConexao
        });
    }

    public async Task<StatusConexao> GetStatusPorUsuarioAsync(int usuarioId)
    {
        var query = @"SELECT 
                        id_status AS IdStatus,
                        id_usuario AS IdUsuario,
                        status AS Status,
                        ultima_verificacao AS UltimaVerificacao,
                        forca_sinal AS ForcaSinal,
                        tipo_conexao AS TipoConexao
                     FROM StatusConexao 
                     WHERE id_usuario = @UsuarioId
                     ORDER BY ultima_verificacao DESC 
                     LIMIT 1";
        
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<StatusConexao>(query, new { UsuarioId = usuarioId }) ?? new StatusConexao
        {
            IdUsuario = usuarioId,
            UltimaVerificacao = DateTime.Now,
            ForcaSinal = 0,
            TipoConexao = string.Empty,
            Status = string.Empty
        };
    }
}