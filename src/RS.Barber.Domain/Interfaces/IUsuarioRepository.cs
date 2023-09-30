using RS.Barber.Domain.Entities;

namespace RS.Barber.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        void Adicionar(Usuario obj);
        void Atualizar(Usuario obj);
        void Remover(Guid id);
        int SaveChanges();
        Task<Usuario> ObterPorIdAsync(Guid id);
        Task<List<Usuario>> ObterTodosAsync();
        Task<List<Usuario>> ObterTodosPaginadoAsync(int s, int t);
        Task<Usuario> ObterPorCpfAsync(string cpf);
        Task<Usuario> ObterPorEmailAsync(string email);
    }
}
