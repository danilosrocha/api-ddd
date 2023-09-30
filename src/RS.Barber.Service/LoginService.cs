using RS.Barber.Domain.Entities;
using RS.Barber.Domain.Interfaces;

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

        public async Task<string> Login(Usuario usuario)
        {
            var token = _tokenService.GerarToken(usuario);
            return token;
        }
    }
}
