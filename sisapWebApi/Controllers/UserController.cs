using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sisapWebApi.Models;
using sisapWebApi.Repositories;

namespace sisapWebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api
        [HttpGet("all")]
        //[Authorize]
        public async Task<ActionResult<List<User>>> GetUsersAsync()
        {
            var usersContext =  new UserRepository();
            var listUsers = await usersContext.getAllUsers().ConfigureAwait(true);
            return listUsers;
        }
    }
}
