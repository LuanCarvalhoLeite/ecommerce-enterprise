using ECommerceEnterprise.Core.Utils;
using ECommerceEnterprise.MessageBus;

namespace ECommerceEnterprise.Pedido.API.Configuration;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
    }
}
