using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;
using Infra.Exceptions;

namespace Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DapperContext _context;

        public UsuarioRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            try
            {
                var query = "SELECT * FROM Usuarios WHERE id_usuario = @Id";
            
                using var connection = _context.CreateConnection();
                var result = await connection.QueryFirstOrDefaultAsync<Usuario>(query, new { Id = id });
        
                return result;
            }
            catch (InfrastructureException ex)
            {
                throw new InfrastructureException("Erro ao buscar usuário por ID.", ex);
            }
        }

        public async Task<Usuario?> GetByIdEmailAsync(string email)
        {
            try
            {
                var query = "SELECT * FROM Usuarios WHERE email = @Email";
            
                using var connection = _context.CreateConnection();
                var result = await connection.QueryFirstOrDefaultAsync<Usuario>(query, new { Email = email });
        
                return result;
            }
            catch (InfrastructureException ex)
            {
                throw new InfrastructureException("Erro ao buscar usuário por e-mail.", ex);
            }
        }
        public async Task<Usuario?> GetByMatriculaAsync(int matricula)
        {
            try
            {
                var query = "SELECT * FROM Usuarios WHERE matricula = @Matricula";
            
                using var connection = _context.CreateConnection();
                var result = await connection.QueryFirstOrDefaultAsync<Usuario>(query, new { Matricula = matricula });
        
                return result;
            }
            catch (InfrastructureException ex)
            {
                throw new InfrastructureException("Erro ao buscar usuário por mátricula.", ex);
            }
        }
        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            try
            {
                 var query = "SELECT * FROM Usuarios";
            
                using var connection = _context.CreateConnection();
                return await connection.QueryAsync<Usuario>(query);
            }
            catch (InfrastructureException ex)
            {
                throw new InfrastructureException("Erro ao buscar usuários.", ex);
            }
           
        }

        public async Task AddAsync(Usuario usuario)
        {
            try
            {
                var query = @"INSERT INTO Usuarios 
                        (matricula, nome, email, senha_hash, cargo, foto_perfil, telefone_corporativo, data_admissao, ativo, data_criacao)
                        VALUES (@Matricula, @Nome, @Email, @Senha_hash, @Cargo, @Foto_perfil, @Telefone_corporativo, @Data_admissao, @Ativo, @DataCriacao);
                        SELECT LAST_INSERT_ID();";
            
                using var connection = _context.CreateConnection();
                usuario.Id_usuario = await connection.ExecuteScalarAsync<int>(query, usuario);
            }
            catch (InfrastructureException ex)
            {
                throw new InfrastructureException("Erro ao adicionar usuário.", ex);
            }
        }

        public async Task UpdateAsync(int id, Usuario usuario)
        {
            try
            {
                var query = @"UPDATE Usuarios SET 
                        matricula = @Matricula,
                        nome = @Nome,
                        email = @Email,
                        senha_hash = @Senha_hash,
                        cargo = @Cargo,
                        foto_perfil = @Foto_perfil,
                        telefone_corporativo = @Telefone_corporativo,
                        data_admissao = @Data_admissao,
                        ativo = @Ativo
                        WHERE id_usuario = @Id";
            
                using var connection = _context.CreateConnection();
                await connection.ExecuteAsync(query, new {
                    usuario.Matricula,
                    usuario.Nome,
                    usuario.Email,
                    usuario.Senha_hash,
                    usuario.Cargo,
                    usuario.Foto_perfil,
                    usuario.Telefone_corporativo,
                    usuario.Data_admissao,
                    usuario.Ativo,
                    Id = id
                });
            }
            catch (InfrastructureException ex)
            {
                throw new InfrastructureException("Erro ao atualizar usuário.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var query = "DELETE FROM Usuarios WHERE id_usuario = @Id";
            
                using var connection = _context.CreateConnection();
                await connection.ExecuteAsync(query, new { Id = id });
            }
            catch (InfrastructureException ex)
            {
                throw new InfrastructureException("Erro ao deletar usuário.", ex);
            }
        }
    }
}