﻿namespace ECommerceEnterprise.WebApp.MVC.Models;

public class ErrorViewModel
{
    public int ErroCode { get; set; }
    public string Titulo { get; set; }
    public string Mensagem { get; set; }
}
public class ResponseResult
{
    public ResponseResult()
    {
        Errors = new ResponseErrorMessages();
    }

    public string Title { get; set; }
    public int Status { get; set; }
    public ResponseErrorMessages Errors { get; set; }
}

public class ResponseErrorMessages
{
    public ResponseErrorMessages()
    {
        Mensagens = new List<string>();
    }

    public List<string> Mensagens { get; set; }
}
