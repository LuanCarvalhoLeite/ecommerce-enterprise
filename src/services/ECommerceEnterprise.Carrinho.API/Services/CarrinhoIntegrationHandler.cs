using ECommerceEnterprise.Carrinho.API.Data;
using ECommerceEnterprise.Core.Messages.Integration;
using ECommerceEnterprise.MessageBus;
using Microsoft.EntityFrameworkCore;

namespace ECommerceEnterprise.Carrinho.API.Services;

public class CarrinhoIntegrationHandler : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public CarrinhoIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
    {
        _serviceProvider = serviceProvider;
        _bus = bus;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetSubscribers();
        return Task.CompletedTask;
    }

    private void SetSubscribers()
    {
        _bus.SubscribeAsync<PedidoRealizadoIntegrationEvent>("PedidoRealizado", async request =>
            await ApagarCarrinho(request));
    }

    private async Task ApagarCarrinho(PedidoRealizadoIntegrationEvent message)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CarrinhoContext>();

        var carrinho = await context.CarrinhoCliente
            .FirstOrDefaultAsync(c => c.ClienteId == message.ClienteId);

        if (carrinho != null)
        {
            context.CarrinhoCliente.Remove(carrinho);
            await context.SaveChangesAsync();
        }
    }
}
