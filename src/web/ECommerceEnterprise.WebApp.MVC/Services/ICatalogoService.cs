using ECommerceEnterprise.WebApp.MVC.Models;
using Refit;

namespace ECommerceEnterprise.WebApp.MVC.Services;

public interface ICatalogoService
{
    Task<IEnumerable<ProdutoViewModel>> ObterTodos();
    Task<ProdutoViewModel> ObterPorId(Guid id);
}
public interface ICatalogoServiceRefit
{
    [Get("/catalogo/produtos/")]
    Task<IEnumerable<ProdutoViewModel>> ObterTodos();

    [Get("/catalogo/produtos/{id}")]
    Task<ProdutoViewModel> ObterPorId(Guid id);
}
