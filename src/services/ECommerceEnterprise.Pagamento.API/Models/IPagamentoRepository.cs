using ECommerceEnterprise.Core.Data;

namespace ECommerceEnterprise.Pagamento.API.Models;

public interface IPagamentoRepository : IRepository<Pagamento>
{
    void AdicionarPagamento(Pagamento pagamento);
    
}
