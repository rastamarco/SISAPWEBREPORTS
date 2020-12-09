using FirebirdSql.Data.FirebirdClient;
using sisapWebApi.Models.Firebird;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sisapWebApi.Repository
{
    public class ProviderRepository
    {
        public async Task<List<Provider>> getAllProviders(string select, FbConnection connection)
        {
            List<Provider> providers = new List<Provider>();
            FbDataReader reader = null;
            FbCommand query = new FbCommand(select, connection);
            reader = query.ExecuteReader();

            while (reader.Read())
            {
                Provider provider = new Provider();
                provider.Cod_Fornecedor= (int)reader["COD_FORNECEDOR"];
                provider.Desc_Fornecedor = reader.GetString(1);
                providers.Add(provider);
            }
            return providers;

        }
    }
}
