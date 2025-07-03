using ECommerceEnterprise.Catalogo.API.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceEnterprise.Catalogo.API.Configuration;

public static class ApiConfig
{
    public static void AddApiConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<CatalogoContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

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
