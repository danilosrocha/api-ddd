using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RS.Barber.Domain.Entities;

public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.ToTable("Enderecos"); // Especifique o nome da tabela
        builder.HasKey(e => e.Id); // Defina a chave primária

        builder.Property(e => e.Rua)
            .IsRequired()
            .HasMaxLength(255); // Configurar a propriedade Rua

        builder.Property(e => e.Complemento)
            .HasMaxLength(255); // Configurar a propriedade Complemento

        // Relacionamento com Cliente (um Endereco pertence a um Cliente)
        builder.HasOne(e => e.Cliente)
            .WithMany(c => c.Enderecos)
            .HasForeignKey(e => e.ClienteId) // Define a chave estrangeira para Cliente
            .IsRequired();
    }
}
