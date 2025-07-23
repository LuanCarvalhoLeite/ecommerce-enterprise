using ECommerceEnterprise.Core.Mediator;
using ECommerceEnterprise.Pedido.API.Application.Commands;
using ECommerceEnterprise.Pedido.API.Application.Events;
using ECommerceEnterprise.Pedido.API.Application.Queries;
using ECommerceEnterprise.Pedidos.Domain.Pedidos;
using ECommerceEnterprise.Pedidos.Domain.Vouchers;
using ECommerceEnterprise.Pedidos.Infra.Data;
using ECommerceEnterprise.Pedidos.Infra.Data.Repository;
using ECommerceEnterprise.WepAPI.Core.Usuario;
using FluentValidation.Results;
using MediatR;

namespace ECommerceEnterprise.Pedido.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void AddRegisterService(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();

        services.AddScoped<IRequestHandler<AdicionarPedidoCommand, ValidationResult>, PedidoCommandHandler>();

        services.AddScoped<INotificationHandler<PedidoRealizadoEvent>, PedidoEventHandler>();

        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IVoucherQueries, VoucherQueries>();
        services.AddScoped<IPedidoQueries, PedidoQueries>();

        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddScoped<IVoucherRepository, VoucherRepository>();
        services.AddScoped<PedidosContext>();
    }
}
