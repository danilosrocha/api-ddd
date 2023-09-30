using Microsoft.EntityFrameworkCore;
using RS.Barber.Domain.Interfaces;
using RS.Barber.Domain.Entities;
using RS.Barber.Infra.Data.Contexts;

namespace RS.Barber.Infra.Data.Repositories
{
    public class ClienteRepository : RepositoryBarber<Cliente>, IClienteRepository
    {
        public ClienteRepository(BarbeariaContext context) : base(context)
        {
        }

        public async Task<Cliente> ObterPorCpfAsync(string cpf)
        {
            return await BuscarUmAsync(e => e.CPF == cpf);
        }

        public override async void Remover(Guid id)
        {
            var cliente = await ObterPorIdAsync(id);
            cliente.DefinirComoExcluido();

            Atualizar(cliente);
        }

        public override async Task<Cliente> ObterPorIdAsync(Guid id)
        {
            return await _db.Clientes.AsNoTracking().Include("Endereco").FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
