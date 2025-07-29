using EasyNetQ;
using ECommerceEnterprise.Core.Messages;
using ECommerceEnterprise.Core.Messages.Integration;
using ECommerceEnterprise.MessageBus;
using ECommerceEnterprise.Pedido.API.Application.DTO;
using ECommerceEnterprise.Pedido.API.Application.Events;
using ECommerceEnterprise.Pedidos.Domain.Pedidos;
using ECommerceEnterprise.Pedidos.Domain.Vouchers;
using ECommerceEnterprise.Pedidos.Domain.Vouchers.Specs;
using FluentValidation.Results;
using MediatR;

namespace ECommerceEnterprise.Pedido.API.Application.Commands;

public class PedidoCommandHandler : CommandHandler,
    IRequestHandler<AdicionarPedidoCommand, ValidationResult>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IVoucherRepository _voucherRepository;
    private readonly IMessageBus _bus;

    public PedidoCommandHandler(IVoucherRepository voucherRepository,
                                IPedidoRepository pedidoRepository,
                                IMessageBus bus)
    {
        _voucherRepository = voucherRepository;
        _pedidoRepository = pedidoRepository;
        _bus = bus;
    }

    public async Task<ValidationResult> Handle(AdicionarPedidoCommand message, CancellationToken cancellationToken)
    {
        // Validação do comando
        if (!message.EhValido()) return message.ValidationResult;

        // Mapear Pedido
        var pedido = MapearPedido(message);

        // Aplicar voucher se houver
        if (!await AplicarVoucher(message, pedido)) return ValidationResult;

        // Validar pedido
        if (!ValidarPedido(pedido)) return ValidationResult;

        // Processar pagamento
        if (!await ProcessarPagamento(pedido, message)) return ValidationResult;

        // Se pagamento tudo ok!
        pedido.AutorizarPedido();

        // Adicionar Evento
        pedido.AdicionarEvento(new PedidoRealizadoEvent(pedido.Id, pedido.ClienteId));

        // Adicionar Pedido Repositorio
        _pedidoRepository.Adicionar(pedido);

        // Persistir dados de pedido e voucher
        return await PersistirDados(_pedidoRepository.UnitOfWork);
    }
    private ECommerceEnterprise.Pedidos.Domain.Pedidos.Pedido MapearPedido(AdicionarPedidoCommand message)
    {
        var endereco = new Endereco
        {
            Logradouro = message.Endereco.Logradouro,
            Numero = message.Endereco.Numero,
            Complemento = message.Endereco.Complemento,
            Bairro = message.Endereco.Bairro,
            Cep = message.Endereco.Cep,
            Cidade = message.Endereco.Cidade,
            Estado = message.Endereco.Estado
        };

        var pedido = new ECommerceEnterprise.Pedidos.Domain.Pedidos.Pedido(message.ClienteId, message.ValorTotal, message.PedidoItems.Select(PedidoItemDTO.ParaPedidoItem).ToList(),
            message.VoucherUtilizado, message.Desconto);

        pedido.AtribuirEndereco(endereco);
        return pedido;
    }

    private async Task<bool> AplicarVoucher(AdicionarPedidoCommand message, ECommerceEnterprise.Pedidos.Domain.Pedidos.Pedido pedido)
    {
        if (!message.VoucherUtilizado) return true;

        var voucher = await _voucherRepository.ObterVoucherPorCodigo(message.VoucherCodigo);
        if (voucher == null)
        {
            AdicionarErro("O voucher informado não existe!");
            return false;
        }

        var voucherValidation = new VoucherValidation().Validate(voucher);
        if (!voucherValidation.IsValid)
        {
            voucherValidation.Errors.ToList().ForEach(m => AdicionarErro(m.ErrorMessage));
            return false;
        }

        pedido.AtribuirVoucher(voucher);
        voucher.DebitarQuantidade();

        _voucherRepository.Atualizar(voucher);

        return true;
    }

    private bool ValidarPedido(ECommerceEnterprise.Pedidos.Domain.Pedidos.Pedido pedido)
    {
        var pedidoValorOriginal = pedido.ValorTotal;
        var pedidoDesconto = pedido.Desconto;

        pedido.CalcularValorPedido();

        if (pedido.ValorTotal != pedidoValorOriginal)
        {
            AdicionarErro("O valor total do pedido não confere com o cálculo do pedido");
            return false;
        }

        if (pedido.Desconto != pedidoDesconto)
        {
            AdicionarErro("O valor total não confere com o cálculo do pedido");
            return false;
        }

        return true;
    }

    public async Task<bool> ProcessarPagamento(ECommerceEnterprise.Pedidos.Domain.Pedidos.Pedido pedido, AdicionarPedidoCommand message)
    {
        var pedidoIniciado = new PedidoIniciadoIntegrationEvent
        {
            PedidoId = pedido.Id,
            ClienteId = pedido.ClienteId,
            Valor = pedido.ValorTotal,
            TipoPagamento = 1, // fixo. Alterar se tiver mais tipos
            NomeCartao = message.NomeCartao,
            NumeroCartao = message.NumeroCartao,
            MesAnoVencimento = message.ExpiracaoCartao,
            CVV = message.CvvCartao
        };

        var result = await _bus
                .RequestAsync<PedidoIniciadoIntegrationEvent, ResponseMessage>(pedidoIniciado);

        if (result.ValidationResult.IsValid) return true;

        foreach (var erro in result.ValidationResult.Errors)
        {
            AdicionarErro(erro.ErrorMessage);
        }

        return false;
    }
}
