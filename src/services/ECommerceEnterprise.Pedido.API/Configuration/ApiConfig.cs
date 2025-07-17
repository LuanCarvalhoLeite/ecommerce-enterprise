using ECommerceEnterprise.Pedidos.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceEnterprise.Pedido.API.Configuration;

public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PedidosContext>(options =>
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
