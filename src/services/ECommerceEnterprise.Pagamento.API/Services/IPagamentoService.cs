using ECommerceEnterprise.Core.Messages.Integration;

namespace ECommerceEnterprise.Pagamento.API.Services;

public interface IPagamentoService
{
    Task<ResponseMessage> AutorizarPagamento(ECommerceEnterprise.Pagamento.API.Models.Pagamento pagamento);
}
