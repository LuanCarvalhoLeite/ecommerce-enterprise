using ECommerceEnterprise.Core.Utils;
using ECommerceEnterprise.MessageBus;
using ECommerceEnterprise.Pagamento.API.Services;

namespace ECommerceEnterprise.Pagamento.API.Configuration;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
            .AddHostedService<PagamentoIntegrationHandler>();
    }
}
