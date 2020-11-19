using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sisapWebApi.Repositories.ReportItems
{
    public class ReportItems
    {
        public List<ReportRepository> reportItems;
        public ReportItems()
        {
            reportItems = new List<ReportRepository>();
            reportItems.Add(new ReportRepository(1, "ApontamentoDeProducao.frx"));
            reportItems.Add(new ReportRepository(11,"ApontamentoDeProducaoR2.frx"));
        }
    }
}
