using FirebirdSql.Data.FirebirdClient;
using sisapWebApi.Models.Firebird;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sisapWebApi.Repository
{
    public class PalletRepository
    {
        public async Task<Pallet> GetPallet(string select, FbConnection connection)
        {

            FbDataReader reader = null;
            FbCommand query = new FbCommand(select, connection);
            reader = query.ExecuteReader();

            Pallet pallet = new Pallet();
            while (reader.Read())
            {
                pallet.nr_pallet = (Int64)reader["NR_PALLET"];
                pallet.id_ativo = reader.GetString(1);
            }
            return pallet;

        }
    }
}
