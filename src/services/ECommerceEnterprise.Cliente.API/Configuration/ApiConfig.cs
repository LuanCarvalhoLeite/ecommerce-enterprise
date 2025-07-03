using ECommerceEnterprise.Cliente.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerceEnterprise.Cliente.API.Configuration;

public static class ApiConfig
{
    public static void AddApiConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<ClientesContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddControllers();

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
