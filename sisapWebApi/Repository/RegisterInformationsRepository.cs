using FirebirdSql.Data.FirebirdClient;
using sisapWebApi.Models.Firebird;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sisapWebApi.Repository
{
    public class RegisterInformationsRepository
    {
        public async Task<Product> GetProductName(string select, FbConnection connection)
        {

            FbDataReader reader = null;
            FbCommand query = new FbCommand(select, connection);
            reader = query.ExecuteReader();

            Product prod = new Product();
            while (reader.Read())
            {
                prod.COD_SICOP = (int)reader["COD_SICOP"];
                prod.DESC_EMBALAGEM = reader.GetString(1);
            }
            return prod;

        }
    }
}
