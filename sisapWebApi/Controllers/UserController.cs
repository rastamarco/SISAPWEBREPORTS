using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sisapWebApi.Models.Firebird;
using sisapWebApi.Services;

namespace sisapWebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api
        [HttpGet("all")]
        //[Authorize]
        public async Task<ActionResult<List<User>>> GetUsersAsync(string filial)
        {
            var usersService =  new UserService();
            var listUsers = await usersService.getAllUsers(filial).ConfigureAwait(true);
            return listUsers;
        }

        [HttpGet("employe/{register}/{filial}")]
        public async Task<ActionResult<Employe>> GetEmployeRegister(int register, string filial)
        {
            var usersService = new UserService();
            var employe = await usersService.getEmployeByRegister(register, filial).ConfigureAwait(true);
            return employe;
        }
    }
}
