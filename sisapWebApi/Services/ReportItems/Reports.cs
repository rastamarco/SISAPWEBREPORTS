using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sisapWebApi.Services.ReportItems
{
    public class Reports
    {
        public List<ReportService> reportItems;
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
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "ApontamentoDeProducao.frx"));
            reportItems.Add(new ReportService(11, "ApontamentoDeProducaoTODOS.frx"));
            reportItems.Add(new ReportService(12, "ApontamentoDeProducaoTURNO-Periodo.frx"));
            reportItems.Add(new ReportService(13, "ApontamentoDeProducaoPERIODO-Turno.frx"));
            reportItems.Add(new ReportService(14, "ApontamentoDeProducaoUIC-Turno.frx"));
            reportItems.Add(new ReportService(15, "ApontamentoDeProducaoUIC.frx"));
        }
        
        public void ReportsExpedicao()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(2, "FormaçãoDePalletExpedicao.frx"));
        }
    }
}
