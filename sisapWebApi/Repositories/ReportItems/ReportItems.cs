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
            reportItems.Add(new ReportRepository(11, "ApontamentoDeProducaoTODOS.frx"));
            reportItems.Add(new ReportRepository(12, "ApontamentoDeProducaoTURNO-Periodo.frx"));
            reportItems.Add(new ReportRepository(13, "ApontamentoDeProducaoPERIODO-Turno.frx"));
            reportItems.Add(new ReportRepository(14, "ApontamentoDeProducaoUIC-Turno.frx"));
            reportItems.Add(new ReportRepository(15, "ApontamentoDeProducaoUIC.frx"));
            reportItems.Add(new ReportRepository(16, "FormaçãoDePalletExpedicao.frx"));
        }
    }
}
