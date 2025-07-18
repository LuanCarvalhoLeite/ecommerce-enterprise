using ECommerceEnterprise.WebApp.MVC.Models;
using ECommerceEnterprise.WebApp.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceEnterprise.WebApp.MVC.Controllers;
[Authorize]
public class CarrinhoController : MainController
{
    private readonly IComprasBffService _comprasBffService;

    public CarrinhoController(IComprasBffService comprasBffService)
    {
       _comprasBffService = comprasBffService;

    }
    [HttpGet]
    [Route("carrinho")]
    public async Task<IActionResult> Index()
    {
        return View(await _comprasBffService.ObterCarrinho());
    }

    [HttpPost]
    [Route("carrinho/adicionar-item")]
    public async Task<IActionResult> AdicionarItemCarrinho(ItemCarrinhoViewModel itemCarrinho)
    {
        var resposta = await _comprasBffService.AdicionarItemCarrinho(itemCarrinho);

        if (ResponsePossuiErros(resposta)) return View("Index", await _comprasBffService.ObterCarrinho());

        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("carrinho/atualizar-item")]
    public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, int quantidade)
    {

        var carrinho = await _comprasBffService.ObterCarrinho();

        var item = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == produtoId);
        if (item == null) return RedirectToAction("Index");

        item.Quantidade = quantidade;

        var resposta = await _comprasBffService.AtualizarItemCarrinho(produtoId, item);

        if (ResponsePossuiErros(resposta)) return View("Index", await _comprasBffService.ObterCarrinho());

        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("carrinho/remover-item")]
    public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
    {
        var resposta = await _comprasBffService.RemoverItemCarrinho(produtoId);

        if (ResponsePossuiErros(resposta)) return View("Index", await _comprasBffService.ObterCarrinho());

        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("carrinho/aplicar-voucher")]
    public async Task<IActionResult> AplicarVoucher(string voucherCodigo)
    {
        var resposta = await _comprasBffService.AplicarVoucherCarrinho(voucherCodigo);

        if (ResponsePossuiErros(resposta)) return View("Index", await _comprasBffService.ObterCarrinho());

        return RedirectToAction("Index");
    }
}
