using ECommerceEnterprise.WebApp.MVC.Models.Usuario;

namespace ECommerceEnterprise.WebApp.MVC.Services.InterfacesUsuario;

public interface IAutenticacaoService
{
    Task<string> Login(UsuarioLoginViewModel usuarioLogin);
    Task<string> Registro(UsuarioRegistroViewModel usuarioRegistro);

}
