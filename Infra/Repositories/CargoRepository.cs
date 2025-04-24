// Infrastructure/Repositories/CargoRepository.cs
using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Infra.Data;

namespace Infra.Repositories;

public class CargoRepository : ICargoRepository
{
    private readonly DapperContext _context;

    public CargoRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Cargo> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();

        var query = "SELECT * FROM Cargos WHERE id_cargo = @Id";
        var result = await connection.QueryFirstOrDefaultAsync<Cargo>(query, new { Id = id });
        return result ?? throw new KeyNotFoundException($"Usuário com ID {id} não encontrado.");

    }

    public async Task<IEnumerable<Cargo>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();

        var query = "SELECT * FROM Cargos ORDER BY nome_cargo";
        return await connection.QueryAsync<Cargo>(query);
    }

    public async Task<Cargo?> GetByNameAsync(string nome)
    {
        using var connection = _context.CreateConnection();

        var query = "SELECT * FROM Cargos WHERE nome_cargo = @Nome";
        return await connection.QueryFirstOrDefaultAsync<Cargo>(query, new { Nome = nome });
    }

    public async Task<int> CreateAsync(Cargo cargo)
    {
        using var connection = _context.CreateConnection();
        
        var query = @"INSERT INTO Cargos (nome_cargo, descricao)
                    VALUES (@Nome_cargo, @Descricao);
                    SELECT LAST_INSERT_ID();";
        
        var id = await connection.ExecuteScalarAsync<int>(query, cargo);

        return id;
    }

    public async Task<bool> UpdateAsync(Cargo cargo)
    {
        using var connection = _context.CreateConnection();
        
        var query = @"UPDATE Cargos SET 
                    nome_cargo = @Nome_cargo,
                    descricao = @Descricao
                    WHERE id_cargo = @Id_cargo";
        
        var affectedRows = await connection.ExecuteAsync(query, cargo);
        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        
        var query = "DELETE FROM Cargos WHERE id_cargo = @Id";
        var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
        return affectedRows > 0;
    }
}