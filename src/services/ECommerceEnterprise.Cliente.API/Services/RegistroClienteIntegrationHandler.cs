using ECommerceEnterprise.Cliente.API.Application.Commands;
using ECommerceEnterprise.Core.Mediator;
using ECommerceEnterprise.Core.Messages.Integration;
using ECommerceEnterprise.MessageBus;
using FluentValidation.Results;
using MediatR;

namespace ECommerceEnterprise.Cliente.API.Services;

public class RegistroClienteIntegrationHandler : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public RegistroClienteIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _bus = bus;
        Console.WriteLine("ServiceProvider recebido: " + serviceProvider.GetType().FullName);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine(">>> Handler do Cliente ATIVO! <<<");
        _bus.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(async request =>
            await RegistrarCliente(request));
            
        return Task.CompletedTask;
    }

    private async Task<ResponseMessage> RegistrarCliente(UsuarioRegistradoIntegrationEvent message)
    {
        var clienteCommand = new RegistrarClienteCommand(message.Id, message.Nome, message.Email, message.Cpf);

        ValidationResult sucesso;

        using (var scope = _serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            sucesso = await mediator.EnviarComando(clienteCommand);
        }
        return new ResponseMessage(sucesso);
    }

}
