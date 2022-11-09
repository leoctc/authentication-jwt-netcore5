using Authentication_JWT_Net5.Repositories;
using Authentication_JWT_Net5.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Authentication_JWT_Net5.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        [HttpPost("authenticate"), AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(string login, string senha)
        {
            // recupera o usuário
            var user = await new UserRepository().GetUser(login, senha);

            // verificar se o usuário existe
            if (user == null)
            {
                return NotFound(new { message = "Credenciais inválidas!" });
            }

            // gera o token 
            var token = TokenService.GenerateToken(user, _configuration["JwtSettings:SecurityKey"]);

            // oculta a senha
            user.Password = string.Empty;

            // retorna os dados
            return Ok(new
            {
                user,
                token
            });
        }

        [HttpGet("anonymous"), AllowAnonymous]        
        public string Anonymous() => "Anônimo";

        [HttpGet()]
        [Route("authenticated")]
        [Authorize(Roles = "manager,employee")]
        public string Authenticated() => $"Autenticado - {User.Identity.Name}";

        [HttpGet("employee")]
        [Authorize(Roles = "manager,employee")]
        public string Employee() => "Funcionário";

        [HttpGet("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";
    }
}
