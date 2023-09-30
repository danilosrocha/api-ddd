using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RS.Barber.Domain.Entities;
using RS.Barber.Domain.Interfaces;
using RS.Barber.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RS.Barber.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly CustomUserService _userManager;

        public UsuarioController(IUsuarioService usuarioService, CustomUserService userManager)
        {
            _usuarioService = usuarioService;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var usuario = await _userManager.GetUserByIdAsync(id);

                if (usuario == null) return NotFound();

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioInput input)
        {
            try
            {
                var resultado = await _userManager.CreateUserAsync(input, input.Password);

                if (resultado.IsNullOrEmpty()) return BadRequest();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid Id, UsuarioInput input)
        {
            try
            {
                var resultado = await _userManager.UpdateUserAsync(Id, input);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }
}
