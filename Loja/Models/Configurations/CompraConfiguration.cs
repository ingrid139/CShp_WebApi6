using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loja.Api.Models.Configurations
{
    public class CompraConfiguration : IEntityTypeConfiguration<Compra>
    {
        public void Configure(EntityTypeBuilder<Compra> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Produtos)
                .WithMany(p => p.Compras)
                .HasForeignKey(d => d.ProdutoId)
                .HasConstraintName("FK_Compra_Produto");

            builder.HasOne(x => x.Cliente)
                .WithMany(p => p.Compras)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK_Compra_Cliente");
        }
    }
}
