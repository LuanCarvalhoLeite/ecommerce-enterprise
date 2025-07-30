using ECommerceEnterprise.Pagamento.API.Models;

namespace ECommerceEnterprise.Pagamento.API.Facade;

public interface IPagamentoFacade
{
    Task<Transacao> AutorizarPagamento(ECommerceEnterprise.Pagamento.API.Models.Pagamento pagamento);
    Task<Transacao> CapturarPagamento(Transacao transacao);
    Task<Transacao> CancelarAutorizacao(Transacao transacao);
}
