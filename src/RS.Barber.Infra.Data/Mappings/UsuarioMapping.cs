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

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.UserName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.Cpf)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(11);


            builder.Ignore(c => c.ValidationResult);
        }
    }
}
    
