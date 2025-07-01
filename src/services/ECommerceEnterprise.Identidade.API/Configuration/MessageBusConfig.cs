using ECommerceEnterprise.Cliente.API.Services;
using ECommerceEnterprise.Core.Utils;
using ECommerceEnterprise.MessageBus;

namespace ECommerceEnterprise.Identidade.API.Configuration;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
            .AddHostedService<RegistroClienteIntegrationHandler>();
    }
}
