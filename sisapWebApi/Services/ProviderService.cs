using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sisapWebApi.Context;
using sisapWebApi.Models.Firebird;
using sisapWebApi.Repository;

namespace sisapWebApi.Services
{
    public class ProviderService
    {

        public async Task<ActionResult<List<Provider>>> GetProviders()
        {

            DataContext dbconnection = new DataContext();
            ProviderRepository repository = new ProviderRepository();
            if (dbconnection.state)
            {
                var result = await repository.getAllProviders("SELECT * FROM TB_CADFORNECEDOR ORDER BY COD_FORNECEDOR", dbconnection.connection).ConfigureAwait(true);
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
