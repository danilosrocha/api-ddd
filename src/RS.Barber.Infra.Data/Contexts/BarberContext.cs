using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RS.Barber.Domain.Entities;
using RS.Barber.Infra.Data.Mappings;

namespace RS.Barber.Infra.Data.Contexts
{
    public class BarbeariaContext : IdentityDbContext<Usuario>
    {
        public BarbeariaContext(DbContextOptions<BarbeariaContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteMapping());
            modelBuilder.ApplyConfiguration(new EnderecoMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());

            modelBuilder.Entity<Usuario>().ToTable("Usuarios").HasKey(t => t.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
