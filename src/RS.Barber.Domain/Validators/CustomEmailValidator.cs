using Microsoft.AspNetCore.Identity;
using RS.Barber.Domain.Entities;
using System.Text.RegularExpressions;

namespace RS.Barber.Domain.Validators
{
    public class CustomEmailValidator<TUser> : IUserValidator<TUser> where TUser : Usuario
    {
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
        {
            var errors = new List<IdentityError>();

            // Verificar se o e-mail é válido usando uma expressão regular
            var emailRegex = new Regex(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
            if (!emailRegex.IsMatch(user.Email))
            {
                errors.Add(new IdentityError
                {
                    Code = "InvalidEmail",
                    Description = "O endereço de e-mail não é válido."
                });
            }

            if (errors.Count > 0)
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}

