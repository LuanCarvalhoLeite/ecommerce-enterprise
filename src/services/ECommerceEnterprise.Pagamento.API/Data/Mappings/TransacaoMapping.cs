﻿using ECommerceEnterprise.Pagamento.API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ECommerceEnterprise.Pagamento.API.Data.Mappings;

public class TransacaoMapping : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.HasKey(c => c.Id);

        // 1 : N => Pagamento : Transacao
        builder.HasOne(c => c.Pagamento)
            .WithMany(c => c.Transacoes);

        builder.ToTable("Transacoes");
    }
}
