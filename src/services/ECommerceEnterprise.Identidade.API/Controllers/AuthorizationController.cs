using EasyNetQ;
using ECommerceEnterprise.Core.Messages.Integration;
using ECommerceEnterprise.Identidade.API.Identidade;
using ECommerceEnterprise.Identidade.API.Models;
using ECommerceEnterprise.Identidade.API.Models.Token;
using ECommerceEnterprise.WepAPI.Core.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerceEnterprise.Identidade.API.Controllers;
[Route("api/identidade[controller]")]
[ApiController]
public class AuthorizationController : MainController
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AppSettings _appSettings; 
    private IBus _bus;

    public AuthorizationController(
            SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager, 
            IOptions<AppSettings> appSettings
        )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _appSettings = appSettings.Value;
    }

    [HttpPost("nova-conta")]
    public async Task<ActionResult> Registrar(UserRegisterViewModel userRegister)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var user = new IdentityUser
        {
            UserName = userRegister.Email,
            Email = userRegister.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, userRegister.Senha);

        if (result.Succeeded)
        {
            var sucesso = await RegistrarCliente(userRegister);
            return CustomResponse(await GerarJwt(userRegister.Email));
        }
        foreach(var error in result.Errors)
        {
            AdicionarErroProcessamento(error.Description);
        }

        return CustomResponse();
    }

    private async Task<ResponseMessage> RegistrarCliente(UserRegisterViewModel userRegister)
    {
        var usuario = await _userManager.FindByEmailAsync(userRegister.Email);

        var usuarioRegistrado = new UsuarioRegistradoIntegrationEvent(
            Guid.Parse(usuario!.Id), userRegister.Nome, userRegister.Email, userRegister.Cpf);

        _bus = RabbitHutch.CreateBus("host=localhost:5672", x => x.EnableNewtonsoftJson());

        var sucesso = await _bus.Rpc.RequestAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(usuarioRegistrado);

        return sucesso;
    }

    [HttpPost("autenticar")]
    public async Task<ActionResult> Login(UserLoginViewModel userLogin)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var result = await _signInManager.PasswordSignInAsync(
            userLogin.Email, 
            userLogin.Senha, 
            isPersistent: false, 
            lockoutOnFailure: true
        );

        if (result.Succeeded) 
        {
            return CustomResponse(await GerarJwt(userLogin.Email));
        }
        if(result.IsLockedOut)
        {
            AdicionarErroProcessamento("Usuário temporariamente bloqueado.");
            return CustomResponse();
        }

        AdicionarErroProcessamento("Usuário ou senha incorretos");
        return CustomResponse();
    }

    private async Task<UserResponseLogin> GerarJwt(string email)
    {
        var user = await _userManager.FindByNameAsync(email);
        var claims = await _userManager.GetClaimsAsync(user!);

        var identityClaims = await ObterClaimsUsuario(claims, user!);
        var encodedToken = CodificarToken(identityClaims);

        return ObterRespostaToken(encodedToken, user!, claims);
    }

    private async Task<ClaimsIdentity> ObterClaimsUsuario(ICollection<Claim> claims, IdentityUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user!.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email!));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

        foreach(var userRole in userRoles)
        {
            claims.Add(new Claim("role", userRole));
        }

        var identityClaims = new ClaimsIdentity(claims);

        return identityClaims;
    }

    private string CodificarToken(ClaimsIdentity identityClaims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Emissor,
            Audience = _appSettings.ValidoEm,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        return tokenHandler.WriteToken(token);
    }

    private UserResponseLogin ObterRespostaToken(string encodedToken, IdentityUser user, IEnumerable<Claim> claims)
    {
        return new UserResponseLogin
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
            UsuarioToken = new UserToken
            {
                Id = user.Id,
                Email = user.Email!,
                Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
            }
        };
    }

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
}
