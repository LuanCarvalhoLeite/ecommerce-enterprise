using ECommerceEnterprise.Bff.Compras.Extensions;
using ECommerceEnterprise.Bff.Compras.Services;
using ECommerceEnterprise.WepAPI.Core.Extensions;
using ECommerceEnterprise.WepAPI.Core.Usuario;
using Polly;

namespace ECommerceEnterprise.Bff.Compras.Configuration;

public static class DependencyInjectionConfig
{
    public static void AddRegisterService(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();

        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<ICatalogoService, CatalogoService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            .AddPolicyHandler(PollyExtensions.EsperarTentar())
            .AddTransientHttpErrorPolicy(
                p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        services.AddHttpClient<ICarrinhoService, CarrinhoService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            .AddPolicyHandler(PollyExtensions.EsperarTentar())
            .AddTransientHttpErrorPolicy(
                p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
    }
}
