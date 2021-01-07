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
        public async Task<ActionResult<List<Chamber>>> GetChambersAsync(string filial)
        {
            var chambersService = new ExpeditionService();
            var listChambers = await chambersService.getAllChambersByLocal(filial).ConfigureAwait(true);
            return listChambers;
        }

        [HttpGet("pallet/{filial}/{nrpallet}")]
        //[Authorize]
        public async Task<ActionResult<dynamic>> GetNrPalletAsync(string filial, int nrpallet)
        {
            var palletService = new ExpeditionService();
            var pallet = await palletService.GetNrPallet(filial, nrpallet).ConfigureAwait(true);
            return (new { npallet = pallet.nr_pallet, iativo = pallet.id_ativo });
        }
    }
}
