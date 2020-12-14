using Microsoft.AspNetCore.Mvc;
using sisapWebApi.Models.Firebird;
using sisapWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace sisapWebApi.Controllers
{
    [Route("api/tickets")]
    [ApiController]
    public class TicketsController : ControllerBase
    {

        [HttpGet("providers/all")]
        //[Authorize]
        public async Task<ActionResult<List<Provider>>> GetProvidersAsync(string filial)
        {
            var providerService = new ProviderService();
            var listProviders = await providerService.GetProviders(filial).ConfigureAwait(true);
            return listProviders;
        }
    }
}
