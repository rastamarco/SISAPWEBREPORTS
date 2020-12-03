using FirebirdSql.Data.FirebirdClient;
using sisapWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sisapWebApi.Repository
{
    public class ChamberRepository
    {

        public async Task<List<Chamber>> getChambersByLocal(string select, FbConnection connection)
        {
            List<Chamber> chambers = new List<Chamber>();
            FbDataReader reader = null;
            FbCommand query = new FbCommand(select, connection);
            reader = query.ExecuteReader();

            while (reader.Read())
            {
                Chamber chamber = new Chamber();
                chamber.cod_camara = reader.GetString(0);
                chamber.desc_camara = reader.GetString(1);
                chamber.cod_filial = reader.GetString(2);
                chambers.Add(chamber);
            }
            return chambers;

        }
    }
}
