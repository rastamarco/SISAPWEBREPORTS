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
    [Route("api/stops")]
    [ApiController]
    public class StopsController : ControllerBase
    {

        [HttpGet("city/all")]
        //[Authorize]
        public async Task<ActionResult<List<City>>> GetProvidersAsync()
        {
            var cityService = new CityService();
            var listCities = await cityService.GetCities().ConfigureAwait(true);
            return listCities;
        }
    }
}
