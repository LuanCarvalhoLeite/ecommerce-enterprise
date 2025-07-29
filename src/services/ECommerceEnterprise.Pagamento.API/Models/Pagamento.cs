using ECommerceEnterprise.Core.DomainObjects;

namespace ECommerceEnterprise.Pagamento.API.Models;

public class Pagamento : Entity, IAgregateRoot
{
    public Pagamento()
    {
        Transacoes = new List<Transacao>();
    }

    public Guid PedidoId { get; set; }
    public TipoPagamento TipoPagamento { get; set; }
    public decimal Valor { get; set; }

    public CartaoCredito CartaoCredito { get; set; }

    // EF Relation
    public ICollection<Transacao> Transacoes { get; set; }

    public void AdicionarTransacao(Transacao transacao)
    {
        Transacoes.Add(transacao);
    }
}
