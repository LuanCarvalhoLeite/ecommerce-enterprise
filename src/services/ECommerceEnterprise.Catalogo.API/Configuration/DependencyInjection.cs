using ECommerceEnterprise.Catalogo.API.Data;
using ECommerceEnterprise.Catalogo.API.Data.Repository;
using ECommerceEnterprise.Catalogo.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceEnterprise.Catalogo.API.Configuration;

public static class DependencyInjection
{
    public static void RegisterService(this IServiceCollection services, IConfiguration configuration)
    {
       services.AddControllers();
       services.AddEndpointsApiExplorer(); 
       services.AddSwaggerGen();

       services.AddDbContext<CatalogoContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

       services.AddScoped<IProdutoRepository, ProdutoRepository>();
       services.AddScoped<CatalogoContext>();
    }
}
