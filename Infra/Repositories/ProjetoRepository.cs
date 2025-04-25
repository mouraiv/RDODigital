using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Infra.Data;
using Infra.Exceptions;

namespace Infra.Repositories;

public class ProjetoRepository : IProjetoRepository
{
    private readonly DapperContext _context;

    public ProjetoRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Projeto?> GetByIdAsync(int id)
    {
        try{
            using var connection = _context.CreateConnection();

            var query = "SELECT * FROM Projetos WHERE id_projeto = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<Projeto>(query, new { Id = id });
            return result;
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao buscar o Projeto com ID {id}: {ex.Message}", ex);
        }

    }

    public async Task<IEnumerable<Projeto>> GetAllAsync()
    {
        try{
            using var connection = _context.CreateConnection();

            var query = "SELECT * FROM Projetos ORDER BY data_criacao DESC";
            return await connection.QueryAsync<Projeto>(query);
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao buscar Projetos: {ex.Message}", ex);
        }
    }
    public async Task<int> CreateAsync(Projeto projeto)
    {
        try
        {
            using var connection = _context.CreateConnection();

            var query = @"INSERT INTO Projetos (
                            titulo_infovia, id_cliente, cidade, mes_referencia,
                            id_fiscal, id_supervisor, data_inicio, data_fim,
                            latitude, longitude, status, progresso_tempo, progresso_projeto
                        )
                        VALUES (
                            @Titulo_infovia, @Id_cliente, @Cidade, @Mes_referencia,
                            @Id_fiscal, @Id_supervisor, @Data_inicio, @Data_fim,
                            @Latitude, @Longitude, @Status, @Progresso_tempo, @Progresso_projeto
                        );
                        SELECT LAST_INSERT_ID();";

            var id = await connection.ExecuteScalarAsync<int>(query, projeto);
            return id;
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao adicionar Projeto: {ex.Message}", ex);
        }
    }

    public async Task<bool> UpdateAsync(Projeto projeto)
    {
        try
        {
            using var connection = _context.CreateConnection();

            var query = @"UPDATE Projetos SET 
                            titulo_infovia = @Titulo_infovia,
                            id_cliente = @Id_cliente,
                            cidade = @Cidade,
                            mes_referencia = @Mes_referencia,
                            id_fiscal = @Id_fiscal,
                            id_supervisor = @Id_supervisor,
                            data_inicio = @Data_inicio,
                            data_fim = @Data_fim,
                            latitude = @Latitude,
                            longitude = @Longitude,
                            status = @Status,
                            progresso_tempo = @Progresso_tempo,
                            progresso_projeto = @Progresso_projeto
                        WHERE id_projeto = @Id_projeto";

            var affectedRows = await connection.ExecuteAsync(query, projeto);
            return affectedRows > 0;
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao atualizar Projeto: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try{
            using var connection = _context.CreateConnection();
        
            var query = "DELETE FROM Projetos WHERE id_projeto = @Id";
            var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
            return affectedRows > 0;

        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao deletar Projeto: {ex.Message}", ex);
        }
    }
}