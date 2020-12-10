using FirebirdSql.Data.FirebirdClient;
using sisapWebApi.Models.Firebird;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sisapWebApi.Repository
{
    public class CityRepository
    {
        public async Task<List<City>> getAllCities(string select, FbConnection connection)
        {
            List<City> cities = new List<City>();
            FbDataReader reader = null;
            FbCommand query = new FbCommand(select, connection);
            reader = query.ExecuteReader();

            while (reader.Read())
            {
                City city = new City();
                city.Cod_Municipio= (int)reader["COD_MUNICIPIO"];
                city.Desc_Municipio = reader.GetString(1);
                city.UF = reader.GetString(2);

                cities.Add(city);
            }
            return cities;

        }
    }
}
