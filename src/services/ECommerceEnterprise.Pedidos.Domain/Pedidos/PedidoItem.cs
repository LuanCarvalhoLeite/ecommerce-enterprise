﻿
using ECommerceEnterprise.Core.DomainObjects;

namespace ECommerceEnterprise.Pedidos.Domain.Pedidos;

public class PedidoItem : Entity
{
    public Guid PedidoId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public string ProdutoNome { get; private set; }
    public int Quantidade { get; private set; }
    public decimal ValorUnitario { get; private set; }
    public string ProdutoImagem { get; set; }

    public Pedido Pedido { get; set; }

    public PedidoItem(Guid produtoId, string produtoNome, int quantidade,
            decimal valorUnitario, string produtoImagem = null)
    {
        ProdutoId = produtoId;
        ProdutoNome = produtoNome;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
        ProdutoImagem = produtoImagem;
    }

    protected PedidoItem() { }

    internal decimal CalcularValor()
    {
        return Quantidade * ValorUnitario;
    }
}
