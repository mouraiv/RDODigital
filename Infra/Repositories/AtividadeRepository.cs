using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Infra.Data;
using Infra.Exceptions;

namespace Infra.Repositories;

public class AtividadeRepository : IAtividadeRepository
{
    private readonly DapperContext _context;

    public AtividadeRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Atividade?> GetByIdAsync(int id)
    {
        try{
            using var connection = _context.CreateConnection();

            var query = "SELECT * FROM Atividades WHERE id_atividade = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<Atividade>(query, new { Id = id });
            return result;
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao buscar o Atividade com ID {id}: {ex.Message}", ex);
        }

    }

    public async Task<IEnumerable<Atividade>> GetAllAsync()
    {
        try{
            using var connection = _context.CreateConnection();

            var query = "SELECT * FROM Atividades ORDER BY item";
            return await connection.QueryAsync<Atividade>(query);
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao buscar Atividades: {ex.Message}", ex);
        }
    }

    public async Task<Atividade?> GetByNameAsync(string nome)
    {
        try{
            using var connection = _context.CreateConnection();

            var query = "SELECT * FROM Atividades WHERE nome_atividade = @Nome";
            return await connection.QueryFirstOrDefaultAsync<Atividade>(query, new { Nome = nome });
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao buscar o Atividade com nome {nome}: {ex.Message}", ex);
        }
    }

    public async Task<int> CreateAsync(Atividade Atividade)
    {
        try{
            using var connection = _context.CreateConnection();

            var query = @"INSERT INTO Atividades (id_cliente, item, classe, nome_atividade, unidade_medida)
                        VALUES (@Id_cliente, @Item, @Classe, @Nome_atividade, @Unidade_medida);
                        SELECT LAST_INSERT_ID();";
            
            var id = await connection.ExecuteScalarAsync<int>(query, Atividade);
            return id;
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao adicionar Atividade: {ex.Message}", ex);
        }
    }

    public async Task<bool> UpdateAsync(Atividade Atividade)
    {
        try{
            using var connection = _context.CreateConnection();

            var query = @"UPDATE Atividades SET 
                        id_cliente = @Id_cliente, item = @Item, classe = @Classe, nome_atividade = @Nome_atividade, unidade_medida = @Unidade_medida
                        WHERE id_atividade = @Id_atividade";
            
            var affectedRows = await connection.ExecuteAsync(query, Atividade);
            return affectedRows > 0;
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao atualizar Atividade: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try{
            using var connection = _context.CreateConnection();
        
            var query = "DELETE FROM Atividades WHERE id_atividade = @Id";
            var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
            return affectedRows > 0;

        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao deletar Atividade: {ex.Message}", ex);
        }
    }
}