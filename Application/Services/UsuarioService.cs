using Application.DTOs;
using Domain.Entities;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Exceptions;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;

        }

        public async Task<UsuarioDTO> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
        }

        public async Task<UsuarioDTO> AddAsync(CreateUsuarioDTO usuarioDto)
        {
            if (usuarioDto == null)
                throw new ArgumentNullException(nameof(usuarioDto));
            
            if (string.IsNullOrWhiteSpace(usuarioDto.Senha))
                throw new ArgumentException("Senha não pode ser vazia");

            // Criar usuário com validação de senha
            var usuario = new Usuario(usuarioDto.Senha);

            _mapper.Map(usuarioDto, usuario);

            // Tratamento manual para campos não mapeados automaticamente
            usuario.Senha_hash = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Senha);
            usuario.DataCriacao = DateTime.UtcNow;
            usuario.Ativo = true;
            
            await _usuarioRepository.AddAsync(usuario);
            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task UpdatePhotoPathAsync(int usuarioId, string pathFile)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado");
            }
            
            usuario.Foto_perfil = pathFile;
            await _usuarioRepository.UpdateAsync(usuarioId, usuario);
        }

        public async Task UpdateAsync(int id, UpdateUsuarioDTO usuarioDto)
        {
            Usuario usuario = await _usuarioRepository.GetByIdAsync(id) ?? 
                throw new KeyNotFoundException("Usuário não encontrado");
            
            // Mapeia apenas os campos não nulos do DTO
            _mapper.Map(usuarioDto, usuario);
            
            await _usuarioRepository.UpdateAsync(id, usuario);
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                throw new KeyNotFoundException("Usuário não encontrado");
            
            await _usuarioRepository.DeleteAsync(id);
        }
    }
}