using sisapWebApi.Context.Interfaces;
using sisapWebApi.Models.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sisapWebApi.Repository
{
    public class RefeitorioRepository : IRefeitorioRepo
    {
        //    public IEnumerable<Refeitorio> GetAllApontamentos()
        //    {
        //        /// Tratar aqui os Chamados
        //        var r = new List<Refeitorio>
        //        {
        //            new Refeitorio{Identidade = 1, NomeRefeitorio="Teste" },
        //            new Refeitorio{Identidade = 2, NomeRefeitorio="Teste" }
        //        };
        //        return r;
        //    }
        public IEnumerable<Refeitorio> GetAllApontamentos()
        {
            throw new NotImplementedException();
        }
    }
}
