﻿using ECommerceEnterprise.WebApp.MVC.Extensions;
using ECommerceEnterprise.WebApp.MVC.Models;
using System.Text.Json;
using System.Text;
using ECommerceEnterprise.Core.Communication;

namespace ECommerceEnterprise.WebApp.MVC.Services;

public abstract class Service
{
    protected StringContent ObterConteudo(object dado)
    {

        return new StringContent(
            JsonSerializer.Serialize(dado),
            Encoding.UTF8,
            "application/json");
    }

    protected async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
    }

    protected bool TratarErrosResponse(HttpResponseMessage response)
    {
        switch ((int)response.StatusCode)
        {
            case 401:
            case 403:
            case 404:
            case 500:
                throw new CustomHttpRequestException(response.StatusCode);

            case 400:
                return false;
        }

        response.EnsureSuccessStatusCode();
        return true;
    }

    protected ResponseResult RetornoOk()
    {
        return new ResponseResult();
    }
}
