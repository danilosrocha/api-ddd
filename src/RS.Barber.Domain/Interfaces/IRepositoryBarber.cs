using RS.Barber.Domain.Entities;
using System.Linq.Expressions;

namespace RS.Barber.Domain.Interfaces
{
    public interface IRepositoryBarber<TEntity> where TEntity : Entity
    {
        void Adicionar(TEntity obj);
        void Atualizar(TEntity obj);
        void Remover(Guid id);
        int SaveChanges();
        Task<TEntity> ObterPorIdAsync(Guid id);
        Task<List<TEntity>> ObterTodosAsync();
        Task<List<TEntity>> ObterTodosPaginadoAsync(int s, int t);
        Task<List<TEntity>> BuscarAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> BuscarUmAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
