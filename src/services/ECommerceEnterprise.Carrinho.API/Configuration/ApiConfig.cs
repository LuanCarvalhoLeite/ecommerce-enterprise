using ECommerceEnterprise.Carrinho.API.Data;
using ECommerceEnterprise.WepAPI.Core.Identidade;
using Microsoft.EntityFrameworkCore;

namespace ECommerceEnterprise.Carrinho.API.Configuration;

public static class ApiConfig
{
    public static void AddApiConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CarrinhoContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddControllers()
            .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
     });

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

        services.AddJwtConfiguration(configuration);
    }
}
