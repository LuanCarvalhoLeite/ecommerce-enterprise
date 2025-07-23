using ECommerceEnterprise.Core.Messages.Integration;
using ECommerceEnterprise.MessageBus;
using MediatR;

namespace ECommerceEnterprise.Pedido.API.Application.Events;

public class PedidoEventHandler : INotificationHandler<PedidoRealizadoEvent>
{
    private readonly IMessageBus _bus;

    public PedidoEventHandler(IMessageBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(PedidoRealizadoEvent message, CancellationToken cancellationToken)
    {
        await _bus.PublishAsync(new PedidoRealizadoIntegrationEvent(message.ClienteId));
    }
}
