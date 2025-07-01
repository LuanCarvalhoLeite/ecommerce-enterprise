using ECommerceEnterprise.Carrinho.API.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceEnterprise.Carrinho.API.Configuration;

public static class ApiConfig
{
    public static void AddApiConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CarrinhoContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddCors(options =>
        {
            options.AddPolicy("Total",
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
        });
    }
}
