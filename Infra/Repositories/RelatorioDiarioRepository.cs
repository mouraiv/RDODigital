using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Infra.Data;
using Infra.Exceptions;

namespace Infra.Repositories;

public class RelatorioDiarioRepository : IRelatorioDiarioRepository
{
    private readonly DapperContext _context;

    public RelatorioDiarioRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<RelatorioDiario?> GetByIdAsync(int id)
    {
        try{
            using var connection = _context.CreateConnection();

            var query = @"SELECT id_relatorio AS Id, id_projeto, id_usuario, data_hora, id_atividade, quantidade, 
                            latitude, longitude, ultima_sincronizacao, sincronizado FROM RelatoriosDiarios WHERE id_relatorio = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<RelatorioDiario>(query, new { Id = id });
            return result;
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao buscar o relatorio com ID {id}: {ex.Message}", ex);
        }

    }

    public async Task<IEnumerable<RelatorioDiario>> GetAllAsync()
    {
        try{
            using var connection = _context.CreateConnection();

            var query = @"SELECT id_relatorio AS Id, id_projeto, id_usuario, data_hora, id_atividade, quantidade, 
                            latitude, longitude, ultima_sincronizacao, sincronizado FROM RelatoriosDiarios ORDER BY data_hora";
            return await connection.QueryAsync<RelatorioDiario>(query);
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao buscar Relatorio Diarios: {ex.Message}", ex);
        }
    }
    public async Task<int> CreateAsync(RelatorioDiario relatorioDiario)
    {
        try
        {
            using var connection = _context.CreateConnection();

            var query = @"INSERT INTO RelatoriosDiarios (
                            id_projeto, id_usuario, data_hora, id_atividade, quantidade, latitude, longitude, ultima_sincronizacao, sincronizado
                        ) VALUES (
                            @Id_projeto, @Id_usuario, @Data_hora, @Id_atividade, @Quantidade, @Latitude, @Longitude, @Ultima_sincronizacao, @Sincronizado
                        );
                        SELECT LAST_INSERT_ID();";

            var id = await connection.ExecuteScalarAsync<int>(query, relatorioDiario);
            return id;
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao adicionar Relatório Diário: {ex.Message}", ex);
        }
    }

    public async Task<bool> UpdateAsync(RelatorioDiario relatorioDiario)
    {
        try
        {
            using var connection = _context.CreateConnection();

            var query = @"UPDATE RelatoriosDiarios SET  
                            id_projeto = @Id_projeto,
                            id_usuario = @Id_usuario,
                            data_hora = @Data_hora,
                            id_atividade = @Id_atividade,
                            quantidade = @Quantidade,
                            latitude = @Latitude,
                            longitude = @Longitude,
                            ultima_sincronizacao = @Ultima_sincronizacao,
                            sincronizado = @Sincronizado
                        WHERE id_relatorio = @Id";

            var affectedRows = await connection.ExecuteAsync(query, relatorioDiario);
            return affectedRows > 0;
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao atualizar Relatório Diário: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try{
            using var connection = _context.CreateConnection();
        
            var query = "DELETE FROM RelatoriosDiarios WHERE id_relatorio = @Id";
            var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
            return affectedRows > 0;

        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao deletar Relatorio Diario: {ex.Message}", ex);
        }
    }
}