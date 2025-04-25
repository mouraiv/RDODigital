using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Infra.Data;
using Infra.Exceptions;

namespace Infra.Repositories;

public class MaterialRepository : IMaterialRepository
{
    private readonly DapperContext _context;

    public MaterialRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Material?> GetByIdAsync(int id)
    {
        try{
            using var connection = _context.CreateConnection();

            var query = "SELECT * FROM Material WHERE id_material = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<Material>(query, new { Id = id });
            return result;
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao buscar o Material com ID {id}: {ex.Message}", ex);
        }

    }

    public async Task<IEnumerable<Material>> GetAllAsync()
    {
        try{
            using var connection = _context.CreateConnection();

            var query = "SELECT * FROM Material ORDER BY data_hora";
            return await connection.QueryAsync<Material>(query);
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao buscar Materials: {ex.Message}", ex);
        }
    }

    public async Task<Material?> GetByNameAsync(string nome)
    {
        try{
            using var connection = _context.CreateConnection();

            var query = "SELECT * FROM Material WHERE nome_material = @Nome";
            return await connection.QueryFirstOrDefaultAsync<Material>(query, new { Nome = nome });
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao buscar o Material com nome {nome}: {ex.Message}", ex);
        }
    }

    public async Task<int> CreateAsync(Material material)
    {
        try
        {
            using var connection = _context.CreateConnection();

            var query = @"INSERT INTO Material (id_projeto, data_hora, id_atividade, quantidade)
                        VALUES (@Id_projeto, @Data_hora, @Id_atividade, @Quantidade);
                        SELECT LAST_INSERT_ID();";

            var id = await connection.ExecuteScalarAsync<int>(query, material);
            return id;
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao adicionar Material: {ex.Message}", ex);
        }
    }

    public async Task<bool> UpdateAsync(Material material)
    {
        try
        {
            using var connection = _context.CreateConnection();

            var query = @"UPDATE Material SET 
                            id_projeto = @Id_projeto,
                            data_hora = @Data_hora,
                            id_atividade = @Id_atividade,
                            quantidade = @Quantidade
                        WHERE id_material = @Id_material";

            var affectedRows = await connection.ExecuteAsync(query, material);
            return affectedRows > 0;
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao atualizar Material: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try{
            using var connection = _context.CreateConnection();
        
            var query = "DELETE FROM Material WHERE id_material = @Id";
            var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
            return affectedRows > 0;

        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao deletar Material: {ex.Message}", ex);
        }
    }
}