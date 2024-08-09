using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionaUsuario")]
        public async Task<IActionResult> AdicionaUsuario([FromBody] LoginModel login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha) || string.IsNullOrWhiteSpace(login.Cpf))
            {
                return Ok("Falta alguns dados");
            }

            var user = new ApplicationUser
            {
                Email = login.Email,
                UserName = login.Email,
                CPF = login.Cpf
            };

            var result = await _userManager.CreateAsync(user, login.Senha);

            if (result.Errors.Any())
            {
                return Ok(result.Errors);
            }

            // GERAÇÃO DE CONFIGMRAÇÃO
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // RETORNO DO EMAIL
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            var response_Return = await _userManager.ConfirmEmailAsync(user, code);

            if (response_Return.Succeeded)
            {
                return Ok("Uusário Adicionado");
            }

            else
            {
                return Ok("Erro ao confirmar cadastro de usuário!");
            }
        }
    }
}