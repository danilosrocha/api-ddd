using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace RS.Barber.Domain.Validators
{
    public static class PhoneNumberValidator
    {
        public static bool IsValid(string phoneNumber)
        {
            var errors = new List<IdentityError>();

            var phoneRegex = new Regex(@"^\(\d{2}\) \d{5}-\d{4}$");

            if (!phoneRegex.IsMatch(phoneNumber))
            {
                errors.Add(new IdentityError
                {
                    Code = "InvalidPhoneNumber",
                    Description = "O número de telefone não está no formato válido (XX) XXXXX-XXXX."
                });
            }

            if (errors.Count > 0)
            {
                return false;
            }

            return true;

        }
    }
}