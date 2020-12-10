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
    [Route("api/expedition")]
    [ApiController]
    public class ExpeditionController : ControllerBase
    {
        [HttpGet("chambers/{filial}")]
        //[Authorize]
        public async Task<ActionResult<List<Chamber>>> GetUsersAsync(string filial)
        {
            var chambersService = new ChamberService();
            var listChambers = await chambersService.getAllChambersByLocal(filial).ConfigureAwait(true);
            return listChambers;
        }
    }
}
