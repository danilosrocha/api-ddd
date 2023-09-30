using RS.Barber.Application.Token;
using RS.Barber.Domain.Interfaces;
using RS.Barber.Domain.Models;
using RS.Barber.Infra.Data.Repositories;

namespace RS.Barber.Service
{
    public class LoginService : ILoginService
    {
        private readonly ITokenService _tokenService;
        private readonly IUsuarioService _usuarioService;

        public LoginService(ITokenService tokenService, IUsuarioService usuarioService)
        {
            _tokenService = tokenService;
            _usuarioService = usuarioService;
        }

        public async Task<string> Login(LoginInput input)
        {
            var usuario = await (input.Email != null ? _usuarioService.ObterPorEmailAsync(input.Email) : _usuarioService.ObterPorCpfAsync(input.Cpf));

            if (usuario == null || usuario.Password != input.Password)
            {
                return "";
            }

            var token = _tokenService.GerarToken(usuario);
            return token;
        }
    }
}
