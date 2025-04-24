// Infrastructure/Repositories/StatusConexaoRepository.cs
using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;

namespace Infra.Repositories;

public class StatusConexaoRepository : IStatusConexaoRepository
{
    private readonly DapperContext _context;

    public StatusConexaoRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<StatusConexao> GetByIdAsync(int id)
    {
        var query = "SELECT * FROM StatusConexao WHERE Id_Status = @Id";

        using var connection = _context.CreateConnection();
        
        var result = await connection.QueryFirstOrDefaultAsync<StatusConexao>(query, new { Id = id });
        return result ?? throw new KeyNotFoundException($"Usuário com ID {id} não encontrado.");
        
    }

    public async Task<IEnumerable<StatusConexao>> GetByUsuarioIdAsync(int usuarioId)
    {
        using (var connection = _context.CreateConnection())
        {
            var query = "SELECT * FROM StatusConexao WHERE Id_Usuario = @UsuarioId";
            return await connection.QueryAsync<StatusConexao>(query, new { UsuarioId = usuarioId });
        }
    }

    public async Task<int> CreateAsync(StatusConexao status)
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

    public async Task<bool> UpdateAsync(StatusConexao status)
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
                         WHERE Id_Status = @Id_Status";
            
            var affectedRows = await connection.ExecuteAsync(query, status);
            return affectedRows > 0;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            var query = "DELETE FROM StatusConexao WHERE Id_Status = @Id";
            var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
            return affectedRows > 0;
        }
    }

}