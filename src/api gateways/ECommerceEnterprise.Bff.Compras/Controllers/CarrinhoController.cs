using ECommerceEnterprise.Bff.Compras.Models;
using ECommerceEnterprise.Bff.Compras.Services;
using ECommerceEnterprise.WepAPI.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceEnterprise.Bff.Compras.Controllers;
[ApiController]
[Authorize]
public class CarrinhoController : MainController
{
    private readonly ICatalogoService _catalogoService;
    private readonly ICarrinhoService _carrinhoService;

    public CarrinhoController(ICarrinhoService carrinhoService, ICatalogoService catalogoService)
    {
        _carrinhoService = carrinhoService;
        _catalogoService = catalogoService;
    }

    [HttpGet]
    [Route("compras/carrinho")]
    public async Task<IActionResult> Index()
    {
        return CustomResponse(await _carrinhoService.ObterCarrinho());
    }

    [HttpGet]
    [Route("compras/carrinho-quantidade")]
    public async Task<int> ObterQuantidadeCarrinho()
    {
        var quantidade = await _carrinhoService.ObterCarrinho();
        return quantidade?.Itens.Sum(i => i.Quantidade) ?? 0;
    }

    [HttpPost]
    [Route("compras/carrinho/items")]
    public async Task<IActionResult> AdicionarItemCarrinho(ItemCarrinhoDTO itemProduto)
    {

        var produto = await _catalogoService.ObterPorId(itemProduto.ProdutoId);

        await ValidarItemCarrinho(produto, itemProduto.Quantidade);
        if(!OperacaoValida()) return CustomResponse();

        itemProduto.Nome = produto.Nome;
        itemProduto.Valor = produto.Valor;
        itemProduto.Imagem = produto.Imagem;

        var resposta = await _carrinhoService.AdicionarItemCarrinho(itemProduto);

        return CustomResponse(resposta);
    }

    [HttpPut]
    [Route("compras/carrinho/items/{produtoId}")]
    public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, ItemCarrinhoDTO itemProduto)
    {
        var produto = await _catalogoService.ObterPorId(produtoId);

        await ValidarItemCarrinho(produto, itemProduto.Quantidade);
        if (!OperacaoValida()) return CustomResponse();

        var resposta = await _carrinhoService.AtualizarItemCarrinho(produtoId, itemProduto);

        return CustomResponse(resposta);
    }

    [HttpDelete]
    [Route("compras/carrinho/items/{produtoId}")]
    public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
    {
        var produto = await _catalogoService.ObterPorId(produtoId);

        if (produto == null)
        {
            AdicionarErroProcessamento("Produto inexistente!");
            return CustomResponse();
        }

        var resposta = await _carrinhoService.RemoverItemCarrinho(produtoId);

        return CustomResponse();
    }

    private async Task ValidarItemCarrinho(ItemProdutoDTO produto, int quantidade)
    {
        if (produto == null) AdicionarErroProcessamento("Produto Inexixtente!");
        if (quantidade < 1) AdicionarErroProcessamento($"Escolha ao menos uma unidade do produto {produto.Nome}");

        var carrinho = await _carrinhoService.ObterCarrinho();
        var itemCarrinho = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == produto.Id);

        if(itemCarrinho != null && itemCarrinho.Quantidade + quantidade > produto.QuantidadeEstoque)
        {
            AdicionarErroProcessamento($"Quantidade do produto {produto.Nome} é maior que o estoque disponível ({produto.QuantidadeEstoque})");
            return;
        }
        if (quantidade > produto.QuantidadeEstoque) AdicionarErroProcessamento($"Quantidade do produto {produto.Nome} é maior que o estoque disponível ({produto.QuantidadeEstoque})");
    }
}
