using ECommerceEnterprise.Identidade.API.Data;
using ECommerceEnterprise.WepAPI.Core.Identidade;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceEnterprise.Identidade.API.Configuration;

public static class IdentityConfig
{
    public static IServiceCollection AddIdetityConfiguration(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddDefaultIdentity<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


        services.AddJwtConfiguration(configuration);

        return services;
    }
}
