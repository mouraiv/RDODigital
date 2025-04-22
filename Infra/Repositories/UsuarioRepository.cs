using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;

namespace Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DapperContext _context;

        public UsuarioRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Usuarios WHERE id_usuario = @Id";
            
            using var connection = _context.CreateConnection();
            var result = await connection.QueryFirstOrDefaultAsync<Usuario>(query, new { Id = id });
    
            return result ?? throw new KeyNotFoundException($"Usuário com ID {id} não encontrado.");
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            var query = "SELECT * FROM Usuarios";
            
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Usuario>(query);
        }

        public async Task AddAsync(Usuario usuario)
        {
            var query = @"INSERT INTO Usuarios 
                        (matricula, nome, email, senha_hash, cargo, foto_perfil, telefone_corporativo, data_admissao, ativo, data_criacao)
                        VALUES (@Matricula, @Nome, @Email, @Senha_hash, @Cargo, @Foto_perfil, @Telefone_corporativo, @Data_admissao, @Ativo, @DataCriacao);
                        SELECT LAST_INSERT_ID();";
            
            using var connection = _context.CreateConnection();
            usuario.Id_usuario = await connection.ExecuteScalarAsync<int>(query, usuario);
        }

        public async Task UpdateAsync(int id, Usuario usuario)
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

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM Usuarios WHERE id_usuario = @Id";
            
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }

    }
}