using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RS.Barber.Domain.Entities;

namespace RS.Barber.Infra.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios"); 
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Cpf)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(11);

            builder.Property(c => c.Ativo)
                .IsRequired();

            builder.Property(c => c.Excluido)
                .IsRequired();

            builder.HasIndex(c => c.Cpf).IsUnique();

            builder.Ignore(c => c.Password);
            builder.Ignore(c => c.ValidationResult);
        }
    }
}
    
