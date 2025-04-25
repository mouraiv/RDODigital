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
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                throw new NotFoundException($"Usuário com ID {id} não encontrado.");
            }
            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            if (usuarios == null || !usuarios.Any())
            {
                throw new NotFoundException("Nenhum usuário encontrado.");
            }
            return _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
        }

        public async Task<UsuarioDTO> AddAsync(CreateUsuarioDTO usuarioDto, Stream fileStream, string fileName)
        {
            
            if (fileStream == null || fileStream.Length == 0)
            {
                throw new NotFoundException("Nenhuma imagem foi enviada.");
            }

            var usuarioExistente = await _usuarioRepository.GetByIdEmailAsync(usuarioDto.Email ?? string.Empty);

            if (usuarioExistente != null)
            {
                throw new ConflictException("e-mail já cadastrado, escolha outro e-mail.");
            }

            if (string.IsNullOrWhiteSpace(usuarioDto.Senha))
            {
                throw new ConflictException("Senha não pode ser vazia");
            }

            var usuario = _mapper.Map<Usuario>(usuarioDto);
            usuario.ValidarSenha(usuarioDto.Senha);

            if (usuario.Matricula == null)
            {
                throw new AppException("Matrícula do usuário não foi informada.");
            }

            var usuarioMatriculaExistente = await _usuarioRepository.GetByMatriculaAsync(usuario.Matricula.Value);
            if (usuarioMatriculaExistente != null)
                throw new ConflictException("Matrícula já está em uso, escolha outra matrícula.");

            usuario.Senha_hash = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Senha);
            usuario.DataCriacao = DateTime.UtcNow;
            usuario.Ativo = true;
            usuario.Foto_perfil = await ProcessarImagemAsync(fileStream, fileName, usuario.Matricula.Value);

            try
            {
                await _usuarioRepository.AddAsync(usuario);

                string novoCaminho = await _fileUserService.SaveFileAsync(
                    fileStream,
                    fileName,
                    "Usuarios",
                    usuario.Matricula,
                    null);

                return _mapper.Map<UsuarioDTO>(usuario);
            }
            catch (Exception ex)
            {
                throw new AppException($"Erro ao adicionar usuário: {ex.Message}", ex);
            }   
        }

        public async Task UpdatePhotoPathAsync(int usuarioId, string pathFile)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);

            if (usuario == null)
            {
                throw new NotFoundException("Usuário não encontrado");
            }

            try
            {
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
            var usuario = await _usuarioRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Usuário não encontrado");

            if (usuarioDto.Matricula == null)
                throw new NotFoundException("Matrícula do usuário não foi informada.");

            var usuarioMatriculaExistente = await _usuarioRepository.GetByMatriculaAsync(usuarioDto.Matricula.Value);
            if (usuarioMatriculaExistente != null && usuarioMatriculaExistente.Id_usuario != usuario.Id_usuario)
                throw new ConflictException("Matrícula já está em uso, escolha outra matrícula.");

            var usuarioExistente = await _usuarioRepository.GetByIdEmailAsync(usuarioDto.Email ?? string.Empty);
            if (usuarioExistente != null && usuarioExistente?.Id_usuario != usuario.Id_usuario)
                throw new ConflictException("E-mail já cadastrado, escolha outro e-mail.");

            try
            {
                _mapper.Map(usuarioDto, usuario);

                if (fileStream != null && fileStream.Length > 0)
                {
                    _fileUserService.ValidateFile(fileStream, fileName);
                    string novoCaminho = await _fileUserService.SaveFileAsync(fileStream, fileName, "Usuarios", usuario.Matricula, null);
                    usuario.Foto_perfil = novoCaminho;
                }

                await _usuarioRepository.UpdateAsync(id, usuario);
            }
            catch (Exception ex)
            {
                throw new AppException($"Erro ao atualizar usuário: {ex.Message}", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                throw new NotFoundException("Usuário não encontrado");
            }

            try
            {
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
        private async Task<string> ProcessarImagemAsync(Stream fileStream, string fileName, int matricula)
        {
            _fileUserService.ValidateFile(fileStream, fileName);
            return await _fileUserService.SaveFileAsync(fileStream, fileName, "Usuarios", matricula, null);
        }
    }
}