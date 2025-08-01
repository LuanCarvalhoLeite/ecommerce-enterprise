﻿using ECommerceEnterprise.Cliente.API.Application.Commands;
using ECommerceEnterprise.Cliente.API.Application.Events;
using ECommerceEnterprise.Cliente.API.Models;
using ECommerceEnterprise.Clientes.API.Models;
using ECommerceEnterprise.Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace ECommerceEnterprise.Core.Commands;

public class ClienteCommandHandler : CommandHandler, 
    IRequestHandler<RegistrarClienteCommand, ValidationResult>,
    IRequestHandler<AdicionarEnderecoCommand, ValidationResult>
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteCommandHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<ValidationResult> Handle(RegistrarClienteCommand message, CancellationToken cancellationToken)
    {
        if (!message.EhValido()) return message.ValidationResult;

        var cliente = new Client(message.Id, message.Nome, message.Email, message.Cpf);

        var clienteExistente = await _clienteRepository.ObterPorCpf(cliente.Cpf.Numero);

        if (clienteExistente != null)
        {
            AdicionarErro("Este CPF já está em uso.");
            return ValidationResult;
        }

        _clienteRepository.Adicionar(cliente);

        cliente.AdicionarEvento(new ClienteRegistradoEvent(message.Id, message.Nome, message.Email, message.Cpf));

        return await PersistirDados(_clienteRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(AdicionarEnderecoCommand message, CancellationToken cancellationToken)
    {
        if (!message.EhValido()) return message.ValidationResult;

        var endereco = new Endereco(message.Logradouro, message.Numero, message.Complemento, message.Bairro, message.Cep, message.Cidade, message.Estado, message.ClienteId);
        _clienteRepository.AdicionarEndereco(endereco);

        return await PersistirDados(_clienteRepository.UnitOfWork);
    }
}
