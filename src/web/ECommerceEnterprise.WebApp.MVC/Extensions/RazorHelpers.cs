﻿using Microsoft.AspNetCore.Mvc.Razor;
using System.Security.Cryptography;
using System.Text;

namespace ECommerceEnterprise.WebApp.MVC.Extensions;

public static class RazorHelpers
{
    public static string HashEmailForGravatar(this RazorPage page, string email)
    {
        var md5Hasher = MD5.Create();
        var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));
        var sBuilder = new StringBuilder();
        foreach (var t in data)
        {
            sBuilder.Append(t.ToString("x2"));
        }
        return sBuilder.ToString();
    }

    public static string FormatoMoeda(this RazorPage page, decimal valor)
    {
        return valor > 0 ? string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", valor) : "Gratuito";
    }

    public static string MensagemEstoque(this RazorPage page, int quantidade)
    {
        return quantidade > 0 ? $"Apenas {quantidade} em estoque!" : "Produto esgotado!";
    }

    public static string UnidadesPorProduto(this RazorPage page, int unidades)
    {
        return unidades > 1 ? $"{unidades} unidades" : $"{unidades} unidade";
    }

    public static string SelectOptionsPorQuantidade(this RazorPage page, int quantidade, int valorSelecionado = 0)
    {
        var sb = new StringBuilder();
        for (var i = 1; i <= quantidade; i++)
        {
            var selected = "";
            if (i == valorSelecionado) selected = "selected";
            sb.Append($"<option {selected} value='{i}'>{i}</option>");
        }

        return sb.ToString();
    }
}
