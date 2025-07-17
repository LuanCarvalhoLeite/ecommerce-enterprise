using ECommerceEnterprise.Core.Mediator;
using ECommerceEnterprise.Pedidos.Domain.Vouchers;
using ECommerceEnterprise.Pedidos.Infra.Data;
using ECommerceEnterprise.Pedidos.Infra.Data.Repository;
using ECommerceEnterprise.WepAPI.Core.Usuario;

namespace ECommerceEnterprise.Pedido.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void AddRegisterService(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();

        services.AddScoped<IMediatorHandler, MediatorHandler>();

        services.AddScoped<IVoucherRepository, VoucherRepository>();
        services.AddScoped<PedidosContext>();
    }
}
