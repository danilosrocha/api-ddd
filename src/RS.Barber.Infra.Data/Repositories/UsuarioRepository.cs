using Microsoft.EntityFrameworkCore;
using RS.Barber.Domain.Entities;
using RS.Barber.Domain.Interfaces;
using RS.Barber.Infra.Data.Contexts;

namespace RS.Barber.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        protected readonly BarbeariaContext _db;
        protected readonly DbSet<Usuario> _dbSet;

        public UsuarioRepository(BarbeariaContext Db)
        {
            _db = Db;
            _dbSet = Db.Set<Usuario>();
        }

        public virtual void Adicionar(Usuario obj)
        {
            _dbSet.Add(obj);
            SaveChanges();
        }


        public virtual void Atualizar(Usuario obj)
        {
            _dbSet.Update(obj);
            SaveChanges();
        }


        public virtual void Remover(Guid id)
        {
            _dbSet.Remove(_dbSet.Find(id));

            SaveChanges();
        }

        public virtual async Task<Usuario> ObterPorIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<List<Usuario>> ObterTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<List<Usuario>> ObterTodosPaginadoAsync(int s, int t)
        {
            return await _dbSet.Skip(s).Take(t).ToListAsync();
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public async Task<Usuario> ObterPorCpfAsync(string cpf)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Cpf == cpf);
        }

        public async Task<Usuario> ObterPorEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Email == email);
        }
    }
}
