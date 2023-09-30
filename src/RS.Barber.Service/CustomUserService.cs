using Microsoft.AspNetCore.Identity;
using RS.Barber.Domain.Entities;
using AutoMapper;
using RS.Barber.Domain.Models;

public class CustomUserService
{
    private readonly UserManager<Usuario> _userManager;
    private readonly IMapper _mapper;

    public CustomUserService(UserManager<Usuario> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<IdentityResult> CreateUserAsync(UsuarioInput usuarioInput, string password)
    {
        var user = _mapper.Map<Usuario>(usuarioInput);
        var validationResult = user.EhValido();

        user.Cpf = FormatCpf(user.Cpf);
        user.PhoneNumber = FormatPhoneNumber(user.PhoneNumber);

        if (!validationResult)
        {
            var errors = new List<IdentityError>();

            foreach (var error in user.ValidationResult)
            {
                errors.Add(new IdentityError { Code = error.Key, Description = error.Value });
            }

            return IdentityResult.Failed(errors.ToArray());
        }

        return await _userManager.CreateAsync(user, password);
    }

    private string FormatCpf(string cpf)
    {
        return cpf.Trim().Replace(".", "").Replace("-", "");
    }

    private string FormatPhoneNumber(string phoneNumber)
    {
        return phoneNumber.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
    }
}
