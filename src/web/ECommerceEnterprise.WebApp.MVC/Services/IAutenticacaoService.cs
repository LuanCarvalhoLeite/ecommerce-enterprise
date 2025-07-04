using ECommerceEnterprise.WebApp.MVC.Models;

namespace ECommerceEnterprise.WebApp.MVC.Services;

public interface IAutenticacaoService
{
    Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin);

    Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro);
}
