﻿using ECommerceEnterprise.Core.Communication;
using ECommerceEnterprise.WebApp.MVC.Extensions;
using ECommerceEnterprise.WebApp.MVC.Models;
using Microsoft.Extensions.Options;

namespace ECommerceEnterprise.WebApp.MVC.Services;

public interface IAutenticacaoService
{
    Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin);

    Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro);
}
public class AutenticacaoService : Service, IAutenticacaoService
{
    private readonly HttpClient _httpClient;

    public AutenticacaoService(HttpClient httpClient,
                               IOptions<AppSettings> settings)
    {
        httpClient.BaseAddress = new Uri(settings.Value.AutenticacaoUrl);

        _httpClient = httpClient;
    }

    public async Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin)
    {
        var loginContent = ObterConteudo(usuarioLogin);

        var response = await _httpClient.PostAsync("/api/identidade/autenticar", loginContent);

        if (!TratarErrosResponse(response))
        {
            return new UsuarioRespostaLogin
            {
                ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
            };
        }

        return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
    }

    public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro)
    {
        var registroContent = ObterConteudo(usuarioRegistro);

        var response = await _httpClient.PostAsync("/api/identidade/nova-conta", registroContent);

        if (!TratarErrosResponse(response))
        {
            return new UsuarioRespostaLogin
            {
                ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
            };
        }

        return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
    }
}
