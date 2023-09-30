using RS.Barber.Domain.Entities;

namespace RS.Barber.Domain.Interfaces
{
    public interface IClienteRepository : IRepositoryBarber<Cliente>
    {
        Task<Cliente> ObterPorCpfAsync(string cpf);
    }
}
