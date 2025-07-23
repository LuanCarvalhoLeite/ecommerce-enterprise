using ECommerceEnterprise.Cliente.API.Models;
using ECommerceEnterprise.Clientes.API.Models;
using ECommerceEnterprise.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceEnterprise.Cliente.API.Data.Repository;

public class ClienteRepository : IClienteRepository
{
    private readonly ClientesContext _context;

    public ClienteRepository(ClientesContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<Client>> ObterTodos()
    {
        return await _context.Clientes.AsNoTracking().ToListAsync();
    }

    public Task<Client> ObterPorCpf(string cpf)
    {
        return _context.Clientes.FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);
    }

    public void Adicionar(Client cliente)
    {
        _context.Clientes.Add(cliente);
    }

    public async Task<Endereco> ObterEnderecoPorId(Guid id)
    {
        return await _context.Enderecos.FirstOrDefaultAsync(e => e.ClienteId == id);
    }

    public void AdicionarEndereco(Endereco endereco)
    {
        _context.Enderecos.Add(endereco);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
