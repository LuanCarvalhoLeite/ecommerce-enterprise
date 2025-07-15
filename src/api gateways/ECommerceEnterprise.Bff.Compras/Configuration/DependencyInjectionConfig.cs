using ECommerceEnterprise.WepAPI.Core.Usuario;

namespace ECommerceEnterprise.Bff.Compras.Configuration;

public static class DependencyInjectionConfig
{
    public static void AddRegisterService(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();
    }
}
