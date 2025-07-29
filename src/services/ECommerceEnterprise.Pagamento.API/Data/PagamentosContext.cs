using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using FluentValidation.Results;
using ECommerceEnterprise.Core.Messages;
using ECommerceEnterprise.Core.Data;

namespace ECommerceEnterprise.Pagamento.API.Data;

public sealed class PagamentosContext : DbContext, IUnitOfWork
{
    public PagamentosContext(DbContextOptions<PagamentosContext> options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<ECommerceEnterprise.Pagamento.API.Models.Pagamento> Pagamentos { get; set; }
    public DbSet<ECommerceEnterprise.Pagamento.API.Models.Transacao> Transacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PagamentosContext).Assembly);
    }

    public async Task<bool> Commit()
    {
        return await SaveChangesAsync() > 0;
    }
}
