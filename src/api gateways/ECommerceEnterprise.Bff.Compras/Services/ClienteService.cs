﻿using ECommerceEnterprise.Bff.Compras.Extensions;
using ECommerceEnterprise.Bff.Compras.Models;
using Microsoft.Extensions.Options;
using System.Net;

namespace ECommerceEnterprise.Bff.Compras.Services;

public interface IClienteService
{
    Task<EnderecoDTO> ObterEndereco();
}

public class ClienteService : Service, IClienteService
{
    private readonly HttpClient _httpClient;

    public ClienteService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.ClienteUrl);
    }

    public async Task<EnderecoDTO> ObterEndereco()
    {
        var response = await _httpClient.GetAsync("/cliente/endereco/");

        if (response.StatusCode == HttpStatusCode.NotFound) return null;

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<EnderecoDTO>(response);
    }
}
