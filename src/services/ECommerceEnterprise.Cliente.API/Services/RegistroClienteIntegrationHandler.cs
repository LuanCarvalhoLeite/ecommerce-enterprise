using EasyNetQ;
using ECommerceEnterprise.Cliente.API.Application.Commands;
using ECommerceEnterprise.Core.Mediator;
using ECommerceEnterprise.Core.Messages.Integration;
using FluentValidation.Results;

namespace ECommerceEnterprise.Cliente.API.Services;

public class RegistroClienteIntegrationHandler : BackgroundService
{
    private IBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public RegistroClienteIntegrationHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _bus = RabbitHutch.CreateBus("host=localhost:5672", x => x.EnableNewtonsoftJson());
        _bus.Rpc.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(async request =>
            new ResponseMessage(await RegistrarCliente(request)));
            
        return Task.CompletedTask;
    }

    private async Task<ValidationResult> RegistrarCliente(UsuarioRegistradoIntegrationEvent message)
    {
        var clienteCommand = new RegistrarClienteCommand(message.Id, message.Nome, message.Email, message.Cpf);

        ValidationResult sucesso;

        using (var scope = _serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            sucesso = await mediator.EnviarComando(clienteCommand);
        }
        return sucesso;
    }

}
