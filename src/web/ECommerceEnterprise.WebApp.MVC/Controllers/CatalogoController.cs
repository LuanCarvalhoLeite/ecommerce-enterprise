﻿using ECommerceEnterprise.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceEnterprise.WebApp.MVC.Controllers;
public class CatalogoController : MainController
{
    private readonly ICatalogoServiceRefit _catalogoService;

    public CatalogoController(ICatalogoServiceRefit catalogoService)
    {
        _catalogoService = catalogoService;
    }
    [HttpGet]
    [Route("/")]
    [Route("vitrine")]
    public async Task<IActionResult> Index()
    { 
        var produtos = await _catalogoService.ObterTodos();

        return View(produtos);
    }

    [HttpGet]
    [Route("produto-detalhe/{id}")]
    public async Task<IActionResult> ProdutoDetalhe(Guid id)
    {
        var produto = await _catalogoService.ObterPorId(id);

        return View(produto);
    }
}
