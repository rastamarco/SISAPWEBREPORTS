using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sisapWebApi.Context;
using sisapWebApi.Models.Firebird;
using sisapWebApi.Repository;

namespace sisapWebApi.Services
{
    public class CityService
    {

        public async Task<ActionResult<List<City>>> GetCities(string filial)
        {

            DataContext dbconnection = new DataContext(filial);
            CityRepository repository = new CityRepository();
            if (dbconnection.state)
            {
                var result = await repository.getAllCities("SELECT * FROM TB_MUNICIPIO ORDER BY DESC_MUNICIPIO", dbconnection.connection).ConfigureAwait(true);
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
