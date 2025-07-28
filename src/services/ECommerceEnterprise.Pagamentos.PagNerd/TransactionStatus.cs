
namespace ECommerceEnterprise.Pagamentos.PagNerd;

public enum TransactionStatus
{
    Authorized = 1,
    Paid,
    Refused,
    Chargedback,
    Cancelled
}
