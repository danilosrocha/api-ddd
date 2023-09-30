using Microsoft.AspNetCore.Identity;
using RS.Barber.Domain.Entities;
using AutoMapper;
using RS.Barber.Domain.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Json;

public class CustomUserService
{
    private readonly UserManager<Usuario> _userManager;
    private readonly IMapper _mapper;

    public CustomUserService(UserManager<Usuario> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
    private string FormatCpf(string cpf)
    {
        return cpf.Trim().Replace(".", "").Replace("-", "");
    }

    private string FormatPhoneNumber(string phoneNumber)
    {
        return phoneNumber.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
    }

    public async Task<string> CreateUserAsync(UsuarioInput usuarioInput, string password)
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

            throw new Exception(JsonSerializer.Serialize(errors));
        }

        var resultado = await _userManager.CreateAsync(user, password);



        if (!resultado.Succeeded)
        {
            throw new Exception(JsonSerializer.Serialize(resultado.Errors));
        }

        var userId = await _userManager.GetUserIdAsync(user);
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var resultado2 = await _userManager.ConfirmEmailAsync(user, code);

        if (!resultado2.Succeeded)
        {
            throw new Exception(JsonSerializer.Serialize(resultado2.Errors));
        }

        return userId;
    }

    public async Task<Usuario> UpdateUserAsync(Guid Id, UsuarioInput input)
    {

        var usuario = await _userManager.FindByIdAsync(Id.ToString());

        if (usuario == null)
        {
            throw new Exception("Usuário não encontrado");
        }

        usuario.UserName = input.UserName;
        usuario.Email = input.Email;
        usuario.PhoneNumber = input.PhoneNumber;


        if (!usuario.EhValido()) throw new Exception("Dados invalidos");

        await _userManager.UpdateAsync(usuario);

        return usuario;

    }

    public async Task<Usuario> GetUserByIdAsync(Guid Id)
    {
        return await _userManager.FindByIdAsync(Id.ToString());
    }


}
