
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceEnterprise.MessageBus;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services, string connection)
    {
        if (string.IsNullOrEmpty(connection)) throw new ArgumentNullException();

        services.AddSingleton<IMessageBus>(sp => new MessageBus(connection));

        return services;
    }
}
