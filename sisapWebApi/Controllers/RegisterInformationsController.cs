using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sisapWebApi.Models.Firebird;
using sisapWebApi.Services;

namespace sisapWebApi.Controllers
{
    [Route("api/informations/")]
    [ApiController]
    public class RegisterInformationsController : ControllerBase
    {

        [HttpGet("product/{codsicop}/{filial}")]
        public async Task<ActionResult<dynamic>> GetProductName(int codsicop, string filial)
        {
            var productService = new RegisterInformationsService();
            var productResult = await productService.getProductByIdSicop(filial, codsicop).ConfigureAwait(true);
            return (new { cod = productResult.COD_SICOP, embalagem = productResult.DESC_EMBALAGEM });
        }

    }
}
