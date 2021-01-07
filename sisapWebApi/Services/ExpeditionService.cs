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
    public class ExpeditionService
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

        public async Task<Pallet> GetNrPallet(string filial, int nrpallet)
        {

            DataContext dbconnection = new DataContext(filial);
            PalletRepository repository = new PalletRepository();
            if (dbconnection.state)
            {
                var result = await repository.GetPallet("SELECT NR_PALLET, ID_ATIVO FROM TB_PALLET WHERE NR_PALLET = '" + nrpallet + "'", dbconnection.connection).ConfigureAwait(true);
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
