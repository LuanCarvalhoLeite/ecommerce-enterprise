
namespace ECommerceEnterprise.Pagamentos.PagNerd;

public class PagNerdService
{
    public readonly string ApiKey;
    public readonly string EncryptionKey;

    public PagNerdService(string apiKey, string encryptionKey)
    {
        ApiKey = apiKey;
        EncryptionKey = encryptionKey;
    }
}
