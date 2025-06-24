using ECommerceEnterprise.Catalogo.API.Models;
using ECommerceEnterprise.WepAPI.Core.Identidade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceEnterprise.Catalogo.API.Controllers;
[ApiController]
[Authorize]
public class CatalogoController : Controller
{
    private readonly IProdutoRepository _produtoRepository;

    public CatalogoController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }
    [AllowAnonymous]
    [HttpGet("catalogo/produtos")]
    public async Task<IEnumerable<Produto>> Index()
    {
        return await _produtoRepository.Obtertodos();
    }
    [ClaimsAuthorize("Catalogo","Ler")]
    [HttpGet("catalogo/produtos/{id:guid}")]
    public async Task<Produto> ProdutoDetalhe(Guid id)
    {
        return await _produtoRepository.ObterPorId(id);
    }
}
