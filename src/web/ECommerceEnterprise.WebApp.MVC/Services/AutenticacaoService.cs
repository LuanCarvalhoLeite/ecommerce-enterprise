using ECommerceEnterprise.WebApp.MVC.Models.Usuario;
using ECommerceEnterprise.WebApp.MVC.Services.InterfacesUsuario;
using System.Text;
using System.Text.Json;

namespace ECommerceEnterprise.WebApp.MVC.Services;

public class AutenticacaoService : IAutenticacaoService
{
    private readonly HttpClient _httpClient;

    public AutenticacaoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> Login(UsuarioLoginViewModel usuarioLogin)
    {
        var loginContent = new StringContent(
            JsonSerializer.Serialize(usuarioLogin),
            Encoding.UTF8,
            mediaType: "application/json");

        var response = await _httpClient.PostAsync(requestUri: "https://localhost:44321/api/identidadeAuthorization/autenticar", loginContent);

        var teste = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
    }

    public async Task<string> Registro(UsuarioRegistroViewModel usuarioRegistro)
    {
        var registroContent = new StringContent(
            JsonSerializer.Serialize(usuarioRegistro),
            Encoding.UTF8,
            mediaType: "application/json");

        var response = await _httpClient.PostAsync(requestUri: "https://localhost:44321/api/identidadeAuthorization/autenticar", registroContent);

        return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
    }
}
