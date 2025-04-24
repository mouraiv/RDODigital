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
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(
            IUsuarioService usuarioService, 
            ILogger<UsuariosController> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetAll()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return Ok(usuarios);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetById(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> Create(IFormFile file, [FromForm] CreateUsuarioDTO usuarioDto)
        {
            var usuario = await _usuarioService.AddAsync(usuarioDto, file.OpenReadStream(), file.FileName);   
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, new {
                Message = "Usuário criado com sucesso.",
                Usuario = usuario
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateUsuarioDTO usuarioDto, IFormFile file)
        {
            // Atualiza dados do usuário
            await _usuarioService.UpdateAsync(id, usuarioDto, file.OpenReadStream(), file.FileName);
            return Ok(new { Message = "Usuário atualizado com sucesso." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {  
            await _usuarioService.DeleteAsync(id);            
            return Ok(new { Message = "Usuário excluído com sucesso." });
        }

    }
}