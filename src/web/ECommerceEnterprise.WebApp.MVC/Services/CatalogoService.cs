using ECommerceEnterprise.WebApp.MVC.Extensions;
using ECommerceEnterprise.WebApp.MVC.Models;
using Microsoft.Extensions.Options;

namespace ECommerceEnterprise.WebApp.MVC.Services;

public class CatalogoService : Service, ICatalogoServiceRefit
{
    private readonly HttpClient _httpClient;

    public CatalogoService(HttpClient httpClient,
        IOptions<AppSettings> settings)
    {
        httpClient.BaseAddress = new Uri(settings.Value.CatalogoUrl);

        _httpClient = httpClient;
    }

    public async Task<ProdutoViewModel> ObterPorId(Guid id)
    {
        var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<ProdutoViewModel>(response);
    }

    public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
    {
        var response = await _httpClient.GetAsync("/catalogo/produtos/");

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<IEnumerable<ProdutoViewModel>>(response);
    }
}
