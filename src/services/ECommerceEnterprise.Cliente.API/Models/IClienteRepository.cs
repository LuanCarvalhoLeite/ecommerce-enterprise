using ECommerceEnterprise.Clientes.API.Models;
using ECommerceEnterprise.Core.Data;

namespace ECommerceEnterprise.Cliente.API.Models;

public interface IClienteRepository : IRepository<Client>
{
    void Adicionar(Client cliente);
    Task<IEnumerable<Client>> ObterTodos();
    Task<Client> ObterPorCpf(string cpf);
}
