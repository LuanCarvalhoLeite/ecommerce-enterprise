namespace ECommerceEnterprise.Core.Data;
public interface IUnitOfWork
{
    Task<bool> Commit();

}
