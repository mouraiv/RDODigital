using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _ClienteService;

        public ClientesController(IClienteService ClienteService)
        {
            _ClienteService = ClienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetAll()
        {
            var Clientes = await _ClienteService.GetAllAsync();
            return Ok(Clientes);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> GetById(int id)
        {
            var Cliente = await _ClienteService.GetByIdAsync(id);
            return Ok(Cliente);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> Create(IFormFile file, [FromForm] CreateClienteDTO ClienteDto)
        {
            var Cliente = await _ClienteService.AddAsync(ClienteDto, file.OpenReadStream(), file.FileName);   
            return CreatedAtAction(nameof(GetById), new { id = Cliente.Id }, new {
                Message = "Cliente criado com sucesso.",
                Cliente = Cliente
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateClienteDTO ClienteDto, IFormFile file)
        {
            // Atualiza dados do Cliente
            await _ClienteService.UpdateAsync(id, ClienteDto, file.OpenReadStream(), file.FileName);
            return Ok(new { Message = "Cliente atualizado com sucesso." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {  
            await _ClienteService.DeleteAsync(id);            
            return Ok(new { Message = "Cliente excluído com sucesso." });
        }

    }
}