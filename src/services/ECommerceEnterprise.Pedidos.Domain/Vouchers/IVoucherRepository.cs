using ECommerceEnterprise.Core.Data;

namespace ECommerceEnterprise.Pedidos.Domain.Vouchers;
public interface IVoucherRepository : IRepository<Voucher>
{
    Task<Voucher> ObterVoucherPorCodigo(string codigo);
    void Atualizar(Voucher voucher);
}
