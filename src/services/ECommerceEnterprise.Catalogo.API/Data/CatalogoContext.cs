﻿using ECommerceEnterprise.Catalogo.API.Models;
using ECommerceEnterprise.Core.Data;
using ECommerceEnterprise.Core.Messages;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace ECommerceEnterprise.Catalogo.API.Data;

public class CatalogoContext: DbContext, IUnitOfWork
{
    public CatalogoContext(DbContextOptions options) : base(options) { }

    public DbSet<Produto> Produtos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);
    }
    public async Task<bool> Commit()
    {
        return await base.SaveChangesAsync() > 0;
    }
}
