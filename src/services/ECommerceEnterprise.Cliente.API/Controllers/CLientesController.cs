using ECommerceEnterprise.Cliente.API.Application.Commands;
using ECommerceEnterprise.Cliente.API.Models;
using ECommerceEnterprise.Core.Mediator;
using ECommerceEnterprise.WepAPI.Core.Controllers;
using ECommerceEnterprise.WepAPI.Core.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceEnterprise.Cliente.API.Controllers;
[ApiController]
public class ClientesController : MainController
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMediatorHandler _mediator;
    private readonly IAspNetUser _user;

    public ClientesController(IClienteRepository clienteRepository, IMediatorHandler mediator, IAspNetUser user)
    {
        _clienteRepository = clienteRepository;
        _mediator = mediator;
        _user = user;
    }

    [HttpGet("cliente/endereco")]
    public async Task<IActionResult> ObterEndereco()
    {
        var endereco = await _clienteRepository.ObterEnderecoPorId(_user.ObterUserId());

        return endereco == null ? NotFound() : CustomResponse(endereco);
    }

    [HttpPost("cliente/endereco")]
    public async Task<IActionResult> AdicionarEndereco(AdicionarEnderecoCommand endereco)
    {
        endereco.ClienteId = _user.ObterUserId();
        return CustomResponse(await _mediator.EnviarComando(endereco));
    }
}
