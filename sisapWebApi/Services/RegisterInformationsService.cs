using sisapWebApi.Context;
using sisapWebApi.Models.Firebird;
using sisapWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sisapWebApi.Services
{
    public class RegisterInformationsService
    {
        public async Task<Product> getProductByIdSicop(string filial, int codsicop)
        {

            DataContext dbconnection = new DataContext(filial);
            var repository = new RegisterInformationsRepository();
            if (dbconnection.state)
            {
                var result = await repository.GetProductName("SELECT COD_SICOP, DESC_EMBALAGEM FROM TB_CADASTRO WHERE COD_SICOP = '" + codsicop + "'", dbconnection.connection).ConfigureAwait(true);
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
