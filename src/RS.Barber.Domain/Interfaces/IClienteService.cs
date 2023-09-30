using RS.Barber.Domain.Dtos;
using RS.Barber.Domain.Entities;

namespace RS.Barber.Domain.Interfaces
{
    public interface IClienteService
    {
        Task<Cliente> AdicionarClienteAsync(ClienteInput cliente);
    }
}
