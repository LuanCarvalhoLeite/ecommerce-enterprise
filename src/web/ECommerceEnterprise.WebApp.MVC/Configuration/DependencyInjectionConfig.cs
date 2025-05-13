using ECommerceEnterprise.WebApp.MVC.Services;
using ECommerceEnterprise.WebApp.MVC.Services.InterfacesUsuario;

namespace ECommerceEnterprise.WebApp.MVC.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();
    }
}
