using ECommerceEnterprise.Core.Data;

namespace ECommerceEnterprise.Pagamento.API.Models;

public interface IPagamentoRepository : IRepository<Pagamento>
{
    void AdicionarPagamento(Pagamento pagamento);
    void AdicionarTransacao(Transacao transacao);
    Task<Pagamento> ObterPagamentoPorPedidoId(Guid pedidoId);
    Task<IEnumerable<Transacao>> ObterTransacaoesPorPedidoId(Guid pedidoId);

}
