using EasyNetQ.Events;
using ECommerceEnterprise.Core.Messages.Integration;
using ECommerceEnterprise.MessageBus;
using ECommerceEnterprise.Pagamento.API.Models;

namespace ECommerceEnterprise.Pagamento.API.Services;

public class PagamentoIntegrationHandler : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public PagamentoIntegrationHandler(
                        IServiceProvider serviceProvider,
                        IMessageBus bus)
    {
        _serviceProvider = serviceProvider;
        _bus = bus;
    }

    private void SetResponder()
    {
        _bus.RespondAsync<PedidoIniciadoIntegrationEvent, ResponseMessage>(async request =>
            await AutorizarPagamento(request));
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetResponder();
        return Task.CompletedTask;
    }

    private async Task<ResponseMessage> AutorizarPagamento(PedidoIniciadoIntegrationEvent message)
    {
        using var scope = _serviceProvider.CreateScope();
        var pagamentoService = scope.ServiceProvider.GetRequiredService<IPagamentoService>();
        var pagamento = new ECommerceEnterprise.Pagamento.API.Models.Pagamento
        {
            PedidoId = message.PedidoId,
            TipoPagamento = (TipoPagamento)message.TipoPagamento,
            Valor = message.Valor,
            CartaoCredito = new CartaoCredito(
                message.NomeCartao, message.NumeroCartao, message.MesAnoVencimento, message.CVV)
        };

        var response = await pagamentoService.AutorizarPagamento(pagamento);

        return response;
    }
}
