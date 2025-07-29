using ECommerceEnterprise.Pagamento.API.Data;
using ECommerceEnterprise.Pagamento.API.Data.Repository;
using ECommerceEnterprise.Pagamento.API.Facade;
using ECommerceEnterprise.Pagamento.API.Models;
using ECommerceEnterprise.Pagamento.API.Services;
using ECommerceEnterprise.WepAPI.Core.Usuario;

namespace ECommerceEnterprise.Pagamento.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void AddRegisterService(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();

        services.AddScoped<IPagamentoService, PagamentoService>();
        services.AddScoped<IPagamentoFacade, PagamentoCartaoCreditoFacade>();

        services.AddScoped<IPagamentoRepository, PagamentoRepository>();
        services.AddScoped<PagamentosContext>();
    }
}
