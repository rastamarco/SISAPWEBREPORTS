using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using sisapWebApi.Context;
using System.Runtime.CompilerServices;
using sisapWebApi.Authorization;
using System.Text;
using sisapWebApi.Services;
using sisapWebApi.Models.Firebird;

namespace sisapWebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
      
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User authUser)
        {
            var userService= new UserService();
            var user = await userService.getUser(authUser.login, authUser.senha, authUser.codFilial).ConfigureAwait(true);
            if (user == null)
                return NotFound(new { message = "Usuário ou Senha inválidos!" });
            var token = TokenService.GenerateToken(user);
            return (new { user = user, token = token });
        }
    }
}
