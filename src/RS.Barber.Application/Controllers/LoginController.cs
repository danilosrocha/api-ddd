using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RS.Barber.Domain.Entities;
using RS.Barber.Domain.Interfaces;
using RS.Barber.Domain.Models;
using RS.Barber.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RS.Barber.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public LoginController(ILoginService loginService, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _loginService = loginService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post(LoginInput input)
        {


            var resultado = await _signInManager.PasswordSignInAsync(input.Email, input.Password, false, lockoutOnFailure: false);

            if(resultado.Succeeded)
            {
                var userCurrent = await _userManager.FindByEmailAsync(input.Email);

                var token = await _loginService.Login(userCurrent);

                return Ok(token);
            }

            return Unauthorized("Falha no login!");            
        }
    }
}
