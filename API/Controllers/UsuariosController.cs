using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IFileUserService _fileUserService;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(
            IUsuarioService usuarioService, 
            IFileUserService fileUserService,
            ILogger<UsuariosController> logger)
        {
            _fileUserService = fileUserService;
            _usuarioService = usuarioService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetAll()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            if (usuarios == null || !usuarios.Any())
            {
                return NotFound(new { Message = "Nenhum usuário encontrado." });
            }
            return Ok(usuarios);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetById(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound(new { Message = $"Usuário com ID {id} não encontrado." });
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> Create(IFormFile file, [FromQuery] CreateUsuarioDTO usuarioDto)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Nenhuma imagem foi enviada.");

            using (var stream = file.OpenReadStream())
            {
                // Validação inicial
                _fileUserService.ValidateFile(stream, file.FileName);
                
                // Cria usuário primeiro (sem foto)
                var usuario = await _usuarioService.AddAsync(usuarioDto);

                if (usuario == null)
                    return BadRequest("Erro ao criar o usuário.");
                
                // Salva a imagem e atualiza usuário
                await ProcessUserImage(stream, file.FileName, usuario);
                
                return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateUsuarioDTO usuarioDto, IFormFile file)
        {
            var usuarioExistente = await _usuarioService.GetByIdAsync(id);

            if (usuarioExistente == null)
                return NotFound("Usuário não encontrado");

            // Atualiza dados do usuário
            await _usuarioService.UpdateAsync(id, usuarioDto);


            // Processa imagem se foi enviada
            if (file != null && file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    _fileUserService.ValidateFile(stream, file.FileName);
                    await ProcessUserImage(stream, file.FileName, usuarioExistente);
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
                return NotFound("Usuário não encontrado");

            // Remove a imagem associada se existir
            if (!string.IsNullOrEmpty(usuario.Foto_perfil))
            {
                await _fileUserService.DeleteFileAsync(usuario.Foto_perfil);
            }

            await _usuarioService.DeleteAsync(id);

            return NoContent();
        }

        private async Task ProcessUserImage(Stream stream, string fileName, UsuarioDTO usuario)
        {
            if (!string.IsNullOrEmpty(usuario.Foto_perfil))
            {
                await _fileUserService.DeleteFileAsync(usuario.Foto_perfil);
            }

            // Salva nova imagem
            string novoCaminho = await _fileUserService.SaveFileAsync(
                stream, 
                fileName, 
                usuario.Matricula ?? 0);

            // Atualiza caminho no usuário
            await _usuarioService.UpdatePhotoPathAsync(usuario.Id, novoCaminho);
        }
    }
}