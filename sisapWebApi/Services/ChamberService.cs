using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.AspNetCore.Mvc;
using sisapWebApi.Context;
using sisapWebApi.Models.Firebird;
using sisapWebApi.Repository;

namespace sisapWebApi.Services
{
    public class ChamberService
    {

        public async Task<List<Chamber>> getAllChambersByLocal(string filial)
        {

            DataContext dbconnection = new DataContext(filial);
            ChamberRepository repository = new ChamberRepository();
            if (dbconnection.state)
            {
                var result = await repository.getChambersByLocal("SELECT * FROM TB_CAMARA WHERE COD_FILIAL = '" + filial.ToUpper() +"'", dbconnection.connection).ConfigureAwait(true);
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
