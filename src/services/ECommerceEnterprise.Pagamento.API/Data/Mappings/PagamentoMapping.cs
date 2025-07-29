using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ECommerceEnterprise.Pagamento.API.Data.Mappings;

public class PagamentoMapping : IEntityTypeConfiguration<ECommerceEnterprise.Pagamento.API.Models.Pagamento>
{
    public void Configure(EntityTypeBuilder<ECommerceEnterprise.Pagamento.API.Models.Pagamento> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Ignore(c => c.CartaoCredito);

        // 1 : N => Pagamento : Transacao
        builder.HasMany(c => c.Transacoes)
            .WithOne(c => c.Pagamento)
            .HasForeignKey(c => c.PagamentoId);

        builder.ToTable("Pagamentos");
    }
}
