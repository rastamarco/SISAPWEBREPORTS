using sisapWebApi.Reports;
using System.Collections.Generic;

namespace sisapWebApi.Reports
{
    public class ReportModule
    {
        public List<ReportService> reportItems;
        public ReportModule(int typeModule)
        {
            switch (typeModule)
            {
                case 1:
                    ReportsPointing();
                    break;
                case 2:
                    ReportsExpedition();
                    break;
                case 3:
                    ReportsQuality();
                    break;
                case 4:
                    ReportsProduction();
                    break;
                case 5:
                    ReportsTickets();
                    break;
            }
            
        }

        public void ReportsPointing()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "ApontamentoDeProducao.frx"));
            reportItems.Add(new ReportService(11, "ApontamentoDeProducaoTODOS.frx"));
            reportItems.Add(new ReportService(12, "ApontamentoDeProducaoTURNO-Periodo.frx"));
            reportItems.Add(new ReportService(13, "ApontamentoDeProducaoPERIODO-Turno.frx"));
            reportItems.Add(new ReportService(14, "ApontamentoDeProducaoUIC-Turno.frx"));
            reportItems.Add(new ReportService(15, "ApontamentoDeProducaoUIC.frx"));
            reportItems.Add(new ReportService(16, "ApontamentoDeRefeitorio.frx"));
        }
        
        public void ReportsExpedition()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(2, "FormaçãoDePalletExpedicao.frx"));
        }

        public void ReportsQuality()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(3, "QualidadeFomentoMensal.frx"));
        }

        public void ReportsProduction()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(4, "MapaMensalProducao.frx"));
            reportItems.Add(new ReportService(41, "ApontamentoRoboCMS.frx"));
        }
        public void ReportsTickets()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(5, "EtiquetasPallet.frx"));
        }
    }
}
