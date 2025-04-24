using Application.DTOs;
using Domain.Entities;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Application.Exceptions;


namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IFileUserService _fileUserService;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IFileUserService fileUserService,IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _fileUserService = fileUserService;
            _mapper = mapper;

        }

        public async Task<UsuarioDTO> GetByIdAsync(int id)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByIdAsync(id);
                if (usuario == null)
                {
                    throw new NotFoundException($"Usuário com ID {id} não encontrado.");
                }
                return _mapper.Map<UsuarioDTO>(usuario);
            }
            catch (Exception ex)
            {
                throw new AppException($"Erro ao buscar usuário: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            try
            {
                var usuarios = await _usuarioRepository.GetAllAsync();
                if (usuarios == null || !usuarios.Any())
                {
                    throw new NotFoundException("Nenhum usuário encontrado.");
                }
                return _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
            }
            catch (Exception ex)
            {
                throw new AppException($"Erro ao buscar usuários: {ex.Message}", ex);
            }
        }

        public async Task<UsuarioDTO> AddAsync(CreateUsuarioDTO usuarioDto, Stream fileStream, string fileName)
        {
            try
            {
                if (fileStream == null || fileStream.Length == 0)
                {
                    throw new NotFoundException("Nenhuma imagem foi enviada.");
                }

                var usuarioExistente = await _usuarioRepository.GetByIdEmailAsync(usuarioDto.Email ?? string.Empty);
                if (usuarioExistente != null)
                    throw new NotFoundException("Usuário já existe com o mesmo e-mail.");

                if (string.IsNullOrWhiteSpace(usuarioDto.Senha))
                    throw new ArgumentException("Senha não pode ser vazia");

                var usuario = new Usuario(usuarioDto.Senha);
                _mapper.Map(usuarioDto, usuario);

                usuario.Senha_hash = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Senha);
                usuario.DataCriacao = DateTime.UtcNow;
                usuario.Ativo = true;

                await _usuarioRepository.AddAsync(usuario);

                // Processar imagem
                _fileUserService.ValidateFile(fileStream, fileName); // Valida o arquivo no serviço

                string novoCaminho = await _fileUserService.SaveFileAsync(
                    fileStream,
                    fileName,
                    usuario.Matricula ?? 0);

                usuario.Foto_perfil = novoCaminho;

                await _usuarioRepository.UpdateAsync(usuario.Id_usuario, usuario);

                return _mapper.Map<UsuarioDTO>(usuario);
            }
            catch (Exception ex)
            {
                throw new AppException($"Erro ao adicionar usuário: {ex.Message}", ex);
            }   
        }


        public async Task UpdatePhotoPathAsync(int usuarioId, string pathFile)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
                if (usuario == null)
                {
                    throw new NotFoundException("Usuário não encontrado");
                }
                
                usuario.Foto_perfil = pathFile;
                await _usuarioRepository.UpdateAsync(usuarioId, usuario);
            }
            catch (Exception ex)
            {
                throw new AppException($"Erro ao atualizar caminho da imagem: {ex.Message}", ex);
            }
        }

        public async Task UpdateAsync(int id, UpdateUsuarioDTO usuarioDto, Stream fileStream, string fileName)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByIdAsync(id);
                if (usuario == null)
                {
                    throw new NotFoundException("Usuário não encontrado");
                }

                // Mapeia os campos não nulos do DTO
                _mapper.Map(usuarioDto, usuario);

                await _usuarioRepository.UpdateAsync(id, usuario);

                // Processa a imagem se foi enviada
                if (fileStream != null && fileStream.Length > 0)
                {
                    // Valida o arquivo antes de prosseguir
                    _fileUserService.ValidateFile(fileStream, fileName);

                    // Processa a imagem (salva e atualiza o caminho no usuário)
                    string novoCaminho = await _fileUserService.SaveFileAsync(fileStream, fileName, usuario.Matricula ?? 0);
                    
                    // Atualiza o caminho da imagem no usuário
                    usuario.Foto_perfil = novoCaminho;

                    await _usuarioRepository.UpdateAsync(id, usuario);
                }
            }
            catch (Exception ex)
            {
                throw new AppException($"Erro ao atualizar usuário: {ex.Message}", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByIdAsync(id);
                if (usuario == null)
                {
                    throw new NotFoundException("Usuário não encontrado");
                }
                
                await _usuarioRepository.DeleteAsync(id);

                // Remove a imagem associada se existir
                if (!string.IsNullOrEmpty(usuario.Foto_perfil))
                {
                    await _fileUserService.DeleteFileAsync(usuario.Foto_perfil);
                }
            }
            catch (Exception ex)
            {
                throw new AppException($"Erro ao deletar usuário: {ex.Message}", ex);
            }

        }
    }
}