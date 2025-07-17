using ECommerceEnterprise.Core.Data;
using ECommerceEnterprise.Pedidos.Domain.Vouchers;

namespace ECommerceEnterprise.Pedidos.Infra.Data.Repository;

public class VoucherRepository : IVoucherRepository
{
    private readonly PedidosContext _context;

    public VoucherRepository(PedidosContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public void Dispose()
    {
        _context.Dispose();
    }
}
