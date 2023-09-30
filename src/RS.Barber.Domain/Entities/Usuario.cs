using Microsoft.AspNetCore.Identity;
using RS.Barber.Domain.Enums;
using RS.Barber.Domain.Validators;

namespace RS.Barber.Domain.Entities
{
    public class Usuario : IdentityUser
    {
        public Usuario()
        {
            ValidationResult = new Dictionary<string, string>();
        }

        public string Cpf { get; set; }
        public TipoUsuario Tipo { get; set; }
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
