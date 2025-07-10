using ECommerceEnterprise.Cliente.API.Application.Commands;
using ECommerceEnterprise.Core.Mediator;
using ECommerceEnterprise.WepAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceEnterprise.Cliente.API.Controllers;
[ApiController]
public class CLientesController : MainController
{
    private readonly IMediatorHandler _mediatorHandler;
    public CLientesController(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }

    [HttpGet("clientes")]
    public async Task<IActionResult> Index()
    {
        //var result = await _mediatorHandler.EnviarComando(new RegistrarClienteCommand(Guid.NewGuid(), "Jose", "jose@gmail.com", "79566566050"));
        

        return CustomResponse();
    }

    [HttpGet("Teste")]
    public IActionResult Get()
    {
        return Ok("Mediator injetado com sucesso!");
    }
}
