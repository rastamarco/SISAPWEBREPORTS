using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sisapWebApi.Models;
using sisapWebApi.Services;

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
            var chambersContext = new ChamberService();
            var listChambers = await chambersContext.getAllChambersByLocal(filial).ConfigureAwait(true);
            return listChambers;
        }
    }
}
