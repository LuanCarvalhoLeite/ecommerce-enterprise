using ECommerceEnterprise.Bff.Compras.Extensions;
using ECommerceEnterprise.Bff.Compras.Models;
using Microsoft.Extensions.Options;
using System.Net;

namespace ECommerceEnterprise.Bff.Compras.Services;

public interface IPedidoService
{
    Task<VoucherDTO> ObterVoucherPorCodigo(string codigo);
}

public class PedidoService : Service, IPedidoService
{
    private readonly HttpClient _httpClient;

    public PedidoService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.PedidoUrl);
    }

    public async Task<VoucherDTO> ObterVoucherPorCodigo(string codigo)
    {
        var response = await _httpClient.GetAsync($"/voucher/{codigo}/");

        if (response.StatusCode == HttpStatusCode.NotFound) return null;

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<VoucherDTO>(response);
    }
}
