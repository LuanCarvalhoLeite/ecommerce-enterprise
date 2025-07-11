﻿namespace ECommerceEnterprise.WebApp.MVC.Models;

public class CarrinhoViewModel
{
    public decimal ValorTotal { get; set; }
    public List<ItemProdutoViewModel> Itens { get; set; } = new List<ItemProdutoViewModel>();
}

public class ItemProdutoViewModel
{
    public Guid ProdutoId { get; set; }
    public string Nome { get; set; }
    public int Quantidade { get; set; }
    public decimal Valor { get; set; }
    public string Imagem { get; set; }
}
