using ECommerceEnterprise.Pagamento.API.Models;
using ECommerceEnterprise.Pagamentos.PagNerd;
using Microsoft.Extensions.Options;

namespace ECommerceEnterprise.Pagamento.API.Facade;

public class PagamentoCartaoCreditoFacade : IPagamentoFacade
{
    private readonly PagamentoConfig _pagamentoConfig;

    public PagamentoCartaoCreditoFacade(IOptions<PagamentoConfig> pagamentoConfig)
    {
        _pagamentoConfig = pagamentoConfig.Value;
    }

    public async Task<Transacao> AutorizarPagamento(ECommerceEnterprise.Pagamento.API.Models.Pagamento pagamento)
    {
        var pagNerdSvc = new PagNerdService(_pagamentoConfig.DefaultApiKey,
            _pagamentoConfig.DefaultEncryptionKey);

        var cardHashGen = new CardHash(pagNerdSvc)
        {
            CardNumber = pagamento.CartaoCredito.NumeroCartao,
            CardHolderName = pagamento.CartaoCredito.NomeCartao,
            CardExpirationDate = pagamento.CartaoCredito.MesAnoVencimento,
            CardCvv = pagamento.CartaoCredito.CVV
        };
        var cardHash = cardHashGen.Generate();

        var transacao = new Transaction(pagNerdSvc)
        {
            CardHash = cardHash,
            CardNumber = pagamento.CartaoCredito.NumeroCartao,
            CardHolderName = pagamento.CartaoCredito.NomeCartao,
            CardExpirationDate = pagamento.CartaoCredito.MesAnoVencimento,
            CardCvv = pagamento.CartaoCredito.CVV,
            PaymentMethod = PaymentMethod.CreditCard,
            Amount = pagamento.Valor
        };

        return ParaTransacao(await transacao.AuthorizeCardTransaction());
    }
    public static Transacao ParaTransacao(Transaction transaction)
    {
        return new Transacao
        {
            Id = Guid.NewGuid(),
            Status = (StatusTransacao)transaction.Status,
            ValorTotal = transaction.Amount,
            BandeiraCartao = transaction.CardBrand,
            CodigoAutorizacao = transaction.AuthorizationCode,
            CustoTransacao = transaction.Cost,
            DataTransacao = transaction.TransactionDate,
            NSU = transaction.Nsu,
            TID = transaction.Tid
        };
    }
}
