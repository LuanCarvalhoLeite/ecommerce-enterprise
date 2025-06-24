using ECommerceEnterprise.Core.DomainObjects;

namespace ECommerceEnterprise.Core.Data;
public interface IRepository<T> : IDisposable where T : IAgregateRoot
{
}
 