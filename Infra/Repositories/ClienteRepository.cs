using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;
using Infra.Exceptions;

namespace Infra.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DapperContext _context;

        public ClienteRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            try
            {
                var query = "SELECT id_cliente AS Id, nome_cliente, Data_criacao, ativo, foto_perfil FROM Clientes WHERE id_cliente = @Id";
                
                using var connection = _context.CreateConnection();
                var result = await connection.QueryFirstOrDefaultAsync<Cliente>(query, new { Id = id });
        
                return result;
            }
            catch (InfrastructureException ex)
            {
                throw new InfrastructureException("Erro ao buscar cliente por ID.", ex);
            }
        }

        public async Task<Cliente?> GetByIdClienteAsync(string nome_cliente)
        {
            try
            {
                var query = "SELECT id_cliente AS Id, nome_cliente, Data_criacao, ativo, foto_perfil FROM Clientes WHERE nome_cliente = @Nome_cliente";
                
                using var connection = _context.CreateConnection();
                var result = await connection.QueryFirstOrDefaultAsync<Cliente>(query, new { Nome_cliente = nome_cliente });
        
                return result;
            }
            catch (InfrastructureException ex)
            {
                throw new InfrastructureException("Erro ao buscar cliente por nome.", ex);
            }
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            try
            {
                var query = "SELECT id_cliente AS Id, nome_cliente, Data_criacao, ativo, foto_perfil FROM Clientes";
                
                using var connection = _context.CreateConnection();
                var result = await connection.QueryAsync<Cliente>(query);
        
                return result;
            }
            catch (InfrastructureException ex)
            {
                throw new InfrastructureException("Erro ao buscar todos os clientes.", ex);
            }
        }

        public async Task AddAsync(Cliente cliente)
        {
            try
            {
                var query = "INSERT INTO Clientes (nome_cliente, data_criacao, ativo, foto_perfil) VALUES (@Nome_cliente, @Data_criacao, @Ativo, @Foto_perfil); SELECT LAST_INSERT_ID();";
                
                using var connection = _context.CreateConnection();
                await connection.ExecuteAsync(query, cliente);
            }
            catch (InfrastructureException ex)
            {
                throw new InfrastructureException("Erro ao adicionar cliente.", ex);
            }
        }

        public async Task UpdateAsync(int id, Cliente cliente)
        {
            try
            {
                var query = "UPDATE Clientes SET nome_cliente = @Nome_cliente, data_criacao = @Data_criacao, ativo = @Ativo, foto_perfil = @Foto_perfil  WHERE id_cliente = @Id";
                
                using var connection = _context.CreateConnection();
                await connection.ExecuteAsync(query, new { cliente.Nome_cliente, cliente.Data_criacao, cliente.Ativo, cliente.Foto_perfil, Id = id });
            }
            catch (InfrastructureException ex)
            {
                throw new InfrastructureException("Erro ao atualizar cliente.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var query = "DELETE FROM Clientes WHERE id_cliente = @Id";
                
                using var connection = _context.CreateConnection();
                await connection.ExecuteAsync(query, new { Id = id });
            }
            catch (InfrastructureException ex)
            {
                throw new InfrastructureException("Erro ao deletar cliente.", ex);
            }
        }   
    }
}