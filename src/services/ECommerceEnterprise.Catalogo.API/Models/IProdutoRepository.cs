using ECommerceEnterprise.Core.Data;

namespace ECommerceEnterprise.Catalogo.API.Models;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<IEnumerable<Produto>> Obtertodos();
    Task<Produto> ObterPorId(Guid id);
    void Adicionar(Produto produto);
    void Atualizar(Produto produto);
}
