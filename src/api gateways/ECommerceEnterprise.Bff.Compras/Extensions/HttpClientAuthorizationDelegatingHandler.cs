﻿using ECommerceEnterprise.WepAPI.Core.Usuario;
using System.Net.Http.Headers;

namespace ECommerceEnterprise.Bff.Compras.Extensions;

public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly IAspNetUser _aspNetUser;

    public HttpClientAuthorizationDelegatingHandler(IAspNetUser aspNetUser)
    {
        _aspNetUser = aspNetUser;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authorizationHeader = _aspNetUser.ObterHttpContext().Request.Headers["Authorization"];

        if (!string.IsNullOrEmpty(authorizationHeader))
        {
            request.Headers.Add("Authorization", new List<string>() { authorizationHeader });
        }

        var token = _aspNetUser.ObterUserToken();

        if (token != null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
