using ECommerceEnterprise.Core.Messages.Integration;
using ECommerceEnterprise.MessageBus;
using ECommerceEnterprise.Pedido.API.Application.Queries;
using ECommerceEnterprise.Pedidos.Domain.Pedidos;

namespace ECommerceEnterprise.Pedido.API.Services;

public class PedidoOrquestradorIntegrationHandler : IHostedService, IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<PedidoOrquestradorIntegrationHandler> _logger;
    private Timer _timer;

    public PedidoOrquestradorIntegrationHandler(ILogger<PedidoOrquestradorIntegrationHandler> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Serviço de pedidos iniciado.");

        _timer = new Timer(ProcessarPedidos, null, TimeSpan.FromSeconds(10),   
            TimeSpan.FromSeconds(15));

        return Task.CompletedTask;
    }

    private async void ProcessarPedidos(object state)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var pedidoQueries = scope.ServiceProvider.GetRequiredService<IPedidoQueries>();
            var pedido = await pedidoQueries.ObterPedidosAutorizados();

            if (pedido == null) return;

            var bus = scope.ServiceProvider.GetRequiredService<IMessageBus>();

            var pedidoAutorizado = new PedidoAutorizadoIntegrationEvent(pedido.ClienteId, pedido.Id,
                pedido.PedidoItems.ToDictionary(p => p.ProdutoId, p => p.Quantidade));

            await bus.PublishAsync(pedidoAutorizado);

            _logger.LogInformation($"Pedido ID: {pedido.Id} foi encaminhado para baixa no estoque.");

            var pedidoRepository = scope.ServiceProvider.GetRequiredService<IPedidoRepository>();
            var pedidoDomain = await pedidoRepository.ObterPorId(pedido.Id);

            pedidoDomain.FinalizarPedido();

            pedidoRepository.Atualizar(pedidoDomain);
            await pedidoRepository.UnitOfWork.Commit();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Serviço de pedidos finalizado.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
