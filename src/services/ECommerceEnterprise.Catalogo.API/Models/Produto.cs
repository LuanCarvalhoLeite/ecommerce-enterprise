using ECommerceEnterprise.Core.DomainObjects;

namespace ECommerceEnterprise.Catalogo.API.Models;

public class Produto : Entity, IAgregateRoot
{
    public  string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public bool Ativo { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataCadastro { get; set; }
    public string Imagem { get; set; } = string.Empty;
    public int QuantidadeEstoque { get; set; }

}
