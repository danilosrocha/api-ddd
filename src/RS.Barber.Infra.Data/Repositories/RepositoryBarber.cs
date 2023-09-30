using Microsoft.EntityFrameworkCore;
using RS.Barber.Domain.Interfaces;
using RS.Barber.Domain.Entities;
using RS.Barber.Infra.Data.Contexts;
using System.Linq.Expressions;

namespace RS.Barber.Infra.Data.Repositories
{
    public abstract class RepositoryBarber<TEntity> : IRepositoryBarber<TEntity> where TEntity : Entity
    {
        protected readonly BarbeariaContext _db;
        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryBarber(BarbeariaContext Db)
        {
            _db = Db;
            _dbSet = Db.Set<TEntity>();
        }

        public virtual void Adicionar(TEntity obj)
        {
            _dbSet.Add(obj);
            SaveChanges();
        }


        public virtual void Atualizar(TEntity obj)
        {
            _dbSet.Update(obj);
            SaveChanges();
        }


        public virtual void Remover(Guid id)
        {
            _dbSet.Remove(_dbSet.Find(id));

            SaveChanges();
        }

        public virtual async Task<TEntity> ObterPorIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<List<TEntity>> ObterTodosPaginadoAsync(int s, int t)
        {
            return await _dbSet.Skip(s).Take(t).ToListAsync();
        }

        public virtual async Task<List<TEntity>> BuscarAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> BuscarUmAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

    }
}
