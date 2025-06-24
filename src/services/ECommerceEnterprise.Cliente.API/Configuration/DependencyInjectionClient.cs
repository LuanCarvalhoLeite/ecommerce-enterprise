using ECommerceEnterprise.Cliente.API.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceEnterprise.Cliente.API.Configuration;

public static class DependencyInjectionClient
{
    public static void AddRegisterService(this IServiceCollection services, IConfiguration configuration)
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
