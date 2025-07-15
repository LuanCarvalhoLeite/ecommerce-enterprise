using ECommerceEnterprise.Bff.Compras.Extensions;
using Microsoft.Extensions.Options;

namespace ECommerceEnterprise.Bff.Compras.Services;

public interface ICarrinhoService
{
}

public class CarrinhoService : Service, ICarrinhoService
{
    private readonly HttpClient _httpClient;

    public CarrinhoService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.CarrinhoUrl);
    }
}
