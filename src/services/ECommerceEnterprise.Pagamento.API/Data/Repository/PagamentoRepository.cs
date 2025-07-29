using ECommerceEnterprise.Core.Data;
using ECommerceEnterprise.Pagamento.API.Models;

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

    public void Dispose()
    {
        _context.Dispose();
    }
}
