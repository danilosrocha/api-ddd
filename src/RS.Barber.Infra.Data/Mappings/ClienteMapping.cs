using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RS.Barber.Domain.Entities;

namespace RS.Barber.Infra.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes"); // Especifique o nome da tabela
            builder.HasKey(c => c.Id); // Defina a chave primária

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(255); // Configurar a propriedade Nome

            builder.Property(c => c.CPF)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(14); // Configurar a propriedade CPF

            builder.Property(c => c.Ativo)
                .IsRequired(); // Configurar a propriedade Ativo

            builder.Property(c => c.Excluido)
                .IsRequired(); // Configurar a propriedade Excluido

            builder.HasIndex(c => c.CPF).IsUnique();
        }
    }
}
    
