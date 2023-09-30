using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RS.Barber.Domain.Dtos;
using RS.Barber.Domain.Interfaces;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RS.Barber.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        [Authorize(Roles = "Vip")]
        public async Task<IActionResult> Post([FromBody] ClienteInput clienteInput)
        {
            try
            {
                var cliente = await _clienteService.AdicionarClienteAsync(clienteInput);
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                var errorResponse = JsonSerializer.Deserialize<object>(ex.Message);
                return BadRequest(errorResponse);
            }
        }
    }
}
