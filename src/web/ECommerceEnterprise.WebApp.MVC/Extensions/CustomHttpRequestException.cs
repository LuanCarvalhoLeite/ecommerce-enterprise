﻿using System.Net;

namespace ECommerceEnterprise.WebApp.MVC.Extensions;

public class CustomHttpRequestException : Exception
{
    public HttpStatusCode StatusCode;

    public CustomHttpRequestException() { }

    public CustomHttpRequestException(string message, Exception innerException)
        : base(message, innerException) { }

    public CustomHttpRequestException(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }
}
