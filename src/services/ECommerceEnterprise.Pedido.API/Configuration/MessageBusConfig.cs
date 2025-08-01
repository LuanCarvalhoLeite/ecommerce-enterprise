﻿using ECommerceEnterprise.Core.Utils;
using ECommerceEnterprise.MessageBus;
using ECommerceEnterprise.Pedido.API.Services;

namespace ECommerceEnterprise.Pedido.API.Configuration;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
            .AddHostedService<PedidoOrquestradorIntegrationHandler>();
            //.AddHostedService<PedidoIntegrationHandler>();
    }
}
