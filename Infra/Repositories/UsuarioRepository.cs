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
            return await connection.QueryFirstOrDefaultAsync<Usuario>(query, new { Id = id }) ?? throw new KeyNotFoundException($"Usuário com ID {id} não encontrado.");
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
                        VALUES (@Matricula, @Nome, @Email, @SenhaHash, @Cargo, @FotoPerfil, @TelefoneCorporativo, @DataAdmissao, @Ativo, @DataCriacao);
                        SELECT LAST_INSERT_ID();";
            
            using var connection = _context.CreateConnection();
            usuario.Id = await connection.ExecuteScalarAsync<int>(query, usuario);
        }

        public async Task UpdateAsync(int id, Usuario usuario)
        {
            var query = @"UPDATE Usuarios SET 
                        matricula = @Matricula,
                        nome = @Nome,
                        email = @Email,
                        senha_hash = @SenhaHash,
                        cargo = @Cargo,
                        foto_perfil = @FotoPerfil,
                        telefone_corporativo = @TelefoneCorporativo,
                        data_admissao = @DataAdmissao,
                        ativo = @Ativo
                        WHERE id_usuario = @Id";
            
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new {
                usuario.Matricula,
                usuario.Nome,
                usuario.Email,
                usuario.SenhaHash,
                usuario.Cargo,
                usuario.FotoPerfil,
                usuario.TelefoneCorporativo,
                usuario.DataAdmissao,
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