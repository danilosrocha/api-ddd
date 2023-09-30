using RS.Barber.Domain.Entities;
using RS.Barber.Domain.Models;

namespace RS.Barber.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> AdicionarUsuarioAsync(UsuarioInput input);
        Task<Usuario> ObterPorIdAsync(Guid id);
        Task<Usuario> ObterPorEmailAsync(string email);
        Task<Usuario> ObterPorCpfAsync(string email);
    }
}
