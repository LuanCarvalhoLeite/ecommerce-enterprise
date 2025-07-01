using ECommerceEnterprise.Carrinho.API.Data;
using ECommerceEnterprise.WepAPI.Core.Usuario;

namespace ECommerceEnterprise.Carrinho.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void AddRegisterService(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();
        services.AddScoped<CarrinhoContext>();
    }
}
