using ECommerceEnterprise.Catalogo.API.Data;
using ECommerceEnterprise.Catalogo.API.Data.Repository;
using ECommerceEnterprise.Catalogo.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceEnterprise.Catalogo.API.Configuration;

public static class DependencyInjection
{
    public static void AddRegisterService(this IServiceCollection services)
    {
       services.AddScoped<IProdutoRepository, ProdutoRepository>();
       services.AddScoped<CatalogoContext>();
    }
}
