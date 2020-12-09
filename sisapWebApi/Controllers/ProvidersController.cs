﻿using Microsoft.AspNetCore.Mvc;
using sisapWebApi.Models.Firebird;
using sisapWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace sisapWebApi.Controllers
{
    [Route("api/providers")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {

        [HttpGet("all")]
        //[Authorize]
        public async Task<ActionResult<List<Provider>>> GetProvidersAsync()
        {
            var providerService = new ProviderService();
            var listProviders = await providerService.GetProviders().ConfigureAwait(true);
            return listProviders;
        }
    }
}