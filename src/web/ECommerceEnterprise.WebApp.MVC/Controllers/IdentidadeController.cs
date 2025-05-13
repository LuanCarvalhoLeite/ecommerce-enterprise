using ECommerceEnterprise.WebApp.MVC.Models.Usuario;
using ECommerceEnterprise.WebApp.MVC.Services.InterfacesUsuario;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceEnterprise.WebApp.MVC.Controllers;

public class IdentidadeController : Controller
{
    private readonly IAutenticacaoService _autenticacaoService;

    public IdentidadeController(IAutenticacaoService autenticacaoService)
    {
        _autenticacaoService = autenticacaoService;
    }

    [HttpGet]
    [Route("nova-conta")]
    public IActionResult Registro()
    {
        return View();
    }

    [HttpPost]
    [Route("nova-conta")]
    public async Task<IActionResult> Registro(UsuarioRegistroViewModel usuarioRegistro)
    {
        if(!ModelState.IsValid) return View(usuarioRegistro);

        var resposta = await _autenticacaoService.Registro(usuarioRegistro);

        if (false) return View(usuarioRegistro);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Route("login")]
    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(UsuarioLoginViewModel usuarioLogin)
    {
        if (!ModelState.IsValid) return View(usuarioLogin);

         var resposta = await _autenticacaoService.Login(usuarioLogin);

        if (false) return View(usuarioLogin);

        return RedirectToAction("Index", "Home");
    }
     
    [HttpGet]
    [Route("sair")]
    public async Task<IActionResult> Logout()
    {
        return RedirectToAction("Index", "Home");
    }

}
