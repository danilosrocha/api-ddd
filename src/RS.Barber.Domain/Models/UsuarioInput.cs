using RS.Barber.Domain.Enums;

namespace RS.Barber.Domain.Models
{
    public class UsuarioInput
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Cpf { get; set; }
        public TipoUsuario Tipo { get; set; }
    }
}
