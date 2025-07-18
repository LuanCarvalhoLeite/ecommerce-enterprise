
using ECommerceEnterprise.Pedidos.Domain.Vouchers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceEnterprise.Pedidos.Infra.Data.Mappings;

public class VoucherMappings : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.HasKey(c => c.Id);


        builder.Property(c => c.Codigo)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.ToTable("Vouchers");
    }
}
