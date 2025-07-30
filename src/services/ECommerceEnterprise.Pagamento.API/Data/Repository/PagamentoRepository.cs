using ECommerceEnterprise.Core.Data;
using ECommerceEnterprise.Pagamento.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceEnterprise.Pagamento.API.Data.Repository;

public class PagamentoRepository : IPagamentoRepository
{
    private readonly PagamentosContext _context;

    public PagamentoRepository(PagamentosContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public void AdicionarPagamento(ECommerceEnterprise.Pagamento.API.Models.Pagamento pagamento)
    {
        _context.Pagamentos.Add(pagamento);
    }

    public void AdicionarTransacao(Transacao transacao)
    {
        _context.Transacoes.Add(transacao);
    }

    public async Task<ECommerceEnterprise.Pagamento.API.Models.Pagamento> ObterPagamentoPorPedidoId(Guid pedidoId)
    {
        return await _context.Pagamentos.AsNoTracking()
            .FirstOrDefaultAsync(p => p.PedidoId == pedidoId);
    }

    public async Task<IEnumerable<Transacao>> ObterTransacaoesPorPedidoId(Guid pedidoId)
    {
        return await _context.Transacoes.AsNoTracking()
            .Where(t => t.Pagamento.PedidoId == pedidoId).ToListAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
