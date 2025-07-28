
using System.Security.Cryptography;
using System.Text;

namespace ECommerceEnterprise.Pagamentos.PagNerd;

public class CardHash
{
    public CardHash(PagNerdService nerdsPagService)
    {
        PagNerdService = nerdsPagService;
    }

    private readonly PagNerdService PagNerdService;

    public string CardHolderName { get; set; }
    public string CardNumber { get; set; }
    public string CardExpirationDate { get; set; }
    public string CardCvv { get; set; }

    public string Generate()
    {
        using var aesAlg = Aes.Create();

        aesAlg.IV = Encoding.Default.GetBytes(PagNerdService.EncryptionKey);
        aesAlg.Key = Encoding.Default.GetBytes(PagNerdService.ApiKey);

        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using var msEncrypt = new MemoryStream();
        using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            swEncrypt.Write(CardHolderName + CardNumber + CardExpirationDate + CardCvv);
        }

        return Encoding.ASCII.GetString(msEncrypt.ToArray());
    }
}
