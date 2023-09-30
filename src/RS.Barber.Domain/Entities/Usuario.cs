using Microsoft.AspNetCore.Identity;
using RS.Barber.Domain.Enums;

namespace RS.Barber.Domain.Entities
{
    public class Usuario : IdentityUser<Guid>
    {
        public Usuario()
        {
            Id = Guid.NewGuid();
            ValidationResult = new Dictionary<string, string>();
        }

        public string Password { get; set; }
        public string Cpf { get; set; }
        public TipoUsuario Tipo { get; set; }
        public bool Ativo { get; set; }
        public bool Excluido { get; set; }
        public IDictionary<string, string> ValidationResult { get; set; }

        public bool EhValido()
        {
            if (!CpfValidator.IsValid(Cpf)) AdicionarErroValidacao(nameof(Cpf), "CPF inválido");
            if (!PhoneNumberValidator.IsValid(PhoneNumber)) AdicionarErroValidacao(nameof(PhoneNumber), "Telefone inválido");

            return ValidationResult.Count == 0;
        }

        public void AdicionarErroValidacao(string erro, string mensagem)
        {
            ValidationResult.Add(erro, mensagem);
        }
    }

}
