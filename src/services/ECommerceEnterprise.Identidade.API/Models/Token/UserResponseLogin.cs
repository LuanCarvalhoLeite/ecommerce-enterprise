namespace ECommerceEnterprise.Identidade.API.Models.Token;

public class UserResponseLogin
{
    public string AccessToken { get; set; } = string.Empty;
    public double ExpiresIn { get; set; }

    public UserToken? UsuarioToken { get; set; }
}
