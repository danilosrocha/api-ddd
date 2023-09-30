using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
            var usuario = await _usuarioService.ObterPorIdAsync(id);

            return Ok(usuario);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioInput input)
        {
            var resultado = await _userManager.CreateUserAsync(input, input.Password);

            return Ok(resultado);
        }


    }
}
