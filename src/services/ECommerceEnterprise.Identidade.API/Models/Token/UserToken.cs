namespace ECommerceEnterprise.Identidade.API.Models.Token;

public class UserToken
{
    public string Id { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
     
    public IEnumerable<UserClaim>? Claims { get; set; }
}
