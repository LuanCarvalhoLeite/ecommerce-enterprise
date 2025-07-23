using ECommerceEnterprise.WebApp.MVC.Models;
using ECommerceEnterprise.WebApp.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceEnterprise.WebApp.MVC.Controllers;
[Authorize]
public class ClienteController : MainController
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpPost]
    public async Task<IActionResult> NovoEndereco(EnderecoViewModel endereco)
    {

        var response = await _clienteService.AdicionarEndereco(endereco);

        if (ResponsePossuiErros(response)) TempData["Erros"] =
            ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();

        return RedirectToAction("EnderecoEntrega", "Pedido");
    }
}
