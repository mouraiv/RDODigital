// Infrastructure/Repositories/StatusConexaoRepository.cs
using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;
using Infra.Exceptions;

namespace Infra.Repositories;

public class StatusConexaoRepository : IStatusConexaoRepository
{
    private readonly DapperContext _context;

    public StatusConexaoRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<StatusConexao?> GetByIdAsync(int id)
    {
        try{
            var query = @"SELECT Id_Status AS Id, Id_Usuario, Status, Ultima_Verificacao, Forca_Sinal, Tipo_Conexao, Latitude, Longitude FROM StatusConexao WHERE Id_Status = @Id";

            using var connection = _context.CreateConnection();
            
            var result = await connection.QueryFirstOrDefaultAsync<StatusConexao>(query, new { Id = id });
            return result;
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao buscar StatusConexao com ID {id}: {ex.Message}", ex);
        }
        
        
    }

    public async Task<IEnumerable<StatusConexao>> GetByUsuarioIdAsync(int usuarioId)
    {
        try
        {
            var query = "SELECT Id_Status AS Id, Id_Usuario, Status, Ultima_Verificacao, Forca_Sinal, Tipo_Conexao, Latitude, Longitude FROM StatusConexao WHERE Id_Usuario = @UsuarioId";
            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<StatusConexao>(query, new { UsuarioId = usuarioId });
            return result;
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao buscar StatusConexao para o usuário com ID {usuarioId}: {ex.Message}", ex);
        }
    }

    public async Task<int> CreateAsync(StatusConexao status)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"INSERT INTO StatusConexao 
                            (Id_Usuario, Status, Ultima_Verificacao, Forca_Sinal, Tipo_Conexao, Latitude, Longitude) 
                            VALUES 
                            (@Id_Usuario, @Status, @Ultima_Verificacao, @Forca_Sinal, @Tipo_Conexao, @Latitude, @Longitude);
                            SELECT LAST_INSERT_ID();";
                
                return await connection.ExecuteScalarAsync<int>(query, status);
            }
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao criar StatusConexao: {ex.Message}", ex);
        }
    }

    public async Task<bool> UpdateAsync(StatusConexao status)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"UPDATE StatusConexao SET 
                            Status = @Status,
                            Ultima_Verificacao = @Ultima_Verificacao,
                            Forca_Sinal = @Forca_Sinal,
                            Tipo_Conexao = @Tipo_Conexao,
                            Latitude = @Latitude,
                            Longitude = @Longitude
                            WHERE Id_Status = @Id";
                
                var affectedRows = await connection.ExecuteAsync(query, status);
                return affectedRows > 0;
            }
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao atualizar StatusConexao: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "DELETE FROM StatusConexao WHERE Id_Status = @Id";
                var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
                return affectedRows > 0;
            }
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao deletar StatusConexao: {ex.Message}", ex);
        }
    }

}