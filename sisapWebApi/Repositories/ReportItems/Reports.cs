using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sisapWebApi.Repositories.ReportItems
{
    public class Reports
    {
        public List<ReportRepository> reportItems;
        public Reports(int typeModule)
        {
            switch (typeModule)
            {
                case 1:
                    ReportsApontamento();
                    break;
                case 2:
                    ReportsExpedicao();
                    break;
            }
            
        }

        public void ReportsApontamento()
        {
            reportItems = new List<ReportRepository>();
            reportItems.Add(new ReportRepository(1, "ApontamentoDeProducao.frx"));
            reportItems.Add(new ReportRepository(11, "ApontamentoDeProducaoTODOS.frx"));
            reportItems.Add(new ReportRepository(12, "ApontamentoDeProducaoTURNO-Periodo.frx"));
            reportItems.Add(new ReportRepository(13, "ApontamentoDeProducaoPERIODO-Turno.frx"));
            reportItems.Add(new ReportRepository(14, "ApontamentoDeProducaoUIC-Turno.frx"));
            reportItems.Add(new ReportRepository(15, "ApontamentoDeProducaoUIC.frx"));
        }
        
        public void ReportsExpedicao()
        {
            reportItems = new List<ReportRepository>();
            reportItems.Add(new ReportRepository(2, "FormaçãoDePalletExpedicao.frx"));
        }
    }
}
