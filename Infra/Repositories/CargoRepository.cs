using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Infra.Data;
using Infra.Exceptions;

namespace Infra.Repositories;

public class CargoRepository : ICargoRepository
{
    private readonly DapperContext _context;

    public CargoRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Cargo?> GetByIdAsync(int id)
    {
        try
        {
            using var connection = _context.CreateConnection();

            var query = @"SELECT 
                            id_cargo AS Id,
                            nome_cargo,
                            descricao
                          FROM Cargos
                          WHERE id_cargo = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<Cargo>(query, new { Id = id });
            return result;
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao buscar o cargo com ID {id}: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<Cargo>> GetAllAsync()
    {
        try
        {
            using var connection = _context.CreateConnection();

            var query = @"SELECT 
                            id_cargo AS Id,
                            nome_cargo,
                            descricao
                          FROM Cargos
                          ORDER BY nome_cargo";
            return await connection.QueryAsync<Cargo>(query);
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao buscar cargos: {ex.Message}", ex);
        }
    }

    public async Task<Cargo?> GetByNameAsync(string nome)
    {
        try
        {
            using var connection = _context.CreateConnection();

            var query = @"SELECT 
                            id_cargo AS Id,
                            nome_cargo,
                            descricao
                          FROM Cargos
                          WHERE nome_cargo = @Nome";
            return await connection.QueryFirstOrDefaultAsync<Cargo>(query, new { Nome = nome });
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao buscar o cargo com nome {nome}: {ex.Message}", ex);
        }
    }

    public async Task<int> CreateAsync(Cargo cargo)
    {
        try
        {
            using var connection = _context.CreateConnection();

            var query = @"INSERT INTO Cargos (nome_cargo, descricao)
                          VALUES (@Nome_cargo, @Descricao);
                          SELECT LAST_INSERT_ID();";

            var id = await connection.ExecuteScalarAsync<int>(query, cargo);
            return id;
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao adicionar cargo: {ex.Message}", ex);
        }
    }

    public async Task<bool> UpdateAsync(Cargo cargo)
    {
        try
        {
            using var connection = _context.CreateConnection();

            var query = @"UPDATE Cargos SET 
                            nome_cargo = @Nome_cargo,
                            descricao = @Descricao
                          WHERE id_cargo = @Id";
            
            var affectedRows = await connection.ExecuteAsync(query, cargo);
            return affectedRows > 0;
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao atualizar cargo: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            using var connection = _context.CreateConnection();

            var query = "DELETE FROM Cargos WHERE id_cargo = @Id";
            var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
            return affectedRows > 0;
        }
        catch (InfrastructureException ex)
        {
            throw new InfrastructureException($"Erro ao deletar cargo: {ex.Message}", ex);
        }
    }
}
