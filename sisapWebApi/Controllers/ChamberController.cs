using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using sisapWebApi.Services;
using sisapWebApi.Models.Firebird;

namespace sisapWebApi.Controllers
{
    [Route("api/chambers")]
    [ApiController]
    public class ChamberController : ControllerBase
    {
        [HttpGet("{filial}")]
        //[Authorize]
        public async Task<ActionResult<List<Chamber>>> GetUsersAsync(string filial)
        {
            var chambersService = new ChamberService();
            var listChambers = await chambersService.getAllChambersByLocal(filial).ConfigureAwait(true);
            return listChambers;
        }
    }
}
