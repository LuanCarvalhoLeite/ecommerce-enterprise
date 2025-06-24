using ECommerceEnterprise.Catalogo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceEnterprise.Catalogo.API.Controllers;
[ApiController]
public class CatalogoController : Controller
{
    private readonly IProdutoRepository _produtoRepository;

    public CatalogoController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    [HttpGet("catalogo/produtos")]
    public async Task<IEnumerable<Produto>> Index()
    {
        return await _produtoRepository.Obtertodos();
    }

    [HttpGet("catalogo/produtos/{id:guid}")]
    public async Task<Produto> ProdutoDetalhe(Guid id)
    {
        return await _produtoRepository.ObterPorId(id);
    }
}
