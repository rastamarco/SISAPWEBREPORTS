using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
using sisapWebApi.Context;
using sisapWebApi.Models;
using sisapWebApi.Services;

namespace sisapWebApi.Repositories
{
    public class ChamberRepository
    {

        public async Task<List<Chamber>> getAllChambersByLocal(string filial)
        {

            DataContext dbconnection = new DataContext();
            ChamberService service = new ChamberService();
            if (dbconnection.state)
            {
                var result = await service.getChambersByLocal("SELECT * FROM TB_CAMARA WHERE COD_FILIAL = '" + filial.ToUpper() +"'", dbconnection.connection).ConfigureAwait(true);
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
