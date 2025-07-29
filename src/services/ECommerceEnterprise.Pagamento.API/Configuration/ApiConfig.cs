using ECommerceEnterprise.Pagamento.API.Data;
using ECommerceEnterprise.Pagamento.API.Facade;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace ECommerceEnterprise.Pagamento.API.Configuration;

public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PagamentosContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.Configure<PagamentoConfig>(configuration.GetSection("PagamentoConfig"));

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
