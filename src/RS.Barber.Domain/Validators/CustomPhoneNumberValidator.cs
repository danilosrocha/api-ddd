using Microsoft.AspNetCore.Identity;
using RS.Barber.Domain.Entities;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RS.Barber.Domain.Validators
{
    public class CustomPhoneNumberValidator<TUser> : IUserValidator<TUser> where TUser : Usuario
    {
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
        {
            var errors = new List<IdentityError>();

            var phoneRegex = new Regex(@"^\(\d{2}\) \d{5}-\d{4}$"); 

            if (!phoneRegex.IsMatch(user.PhoneNumber))
            {
                errors.Add(new IdentityError
                {
                    Code = "InvalidPhoneNumber",
                    Description = "O número de telefone não está no formato válido (XX) XXXXX-XXXX."
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
