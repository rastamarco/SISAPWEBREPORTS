using sisapWebApi.Reports;
using System.Collections.Generic;

namespace sisapWebApi.Reports
{
    public class ReportModule
    {
        public List<ReportService> reportItems;
        public ReportModule(int typeModule, string filial)
        {
            switch (typeModule)
            {
                case 1:
                    ReportsPointing(filial);
                    break;
                case 2:
                    ReportsExpedition(filial);
                    break;
                case 3:
                    ReportsQuality(filial);
                    break;
                case 4:
                    ReportsProduction(filial);
                    break;
                case 5:
                    ReportsTickets(filial);
                    break;
                case 6:
                    ReportsScheduling(filial);
                    break;
                case 7:
                    ReportsStops(filial);
                    break;
                case 8:
                    ReportsEggs(filial);
                    break;
            }

        }

        public void ReportsPointing(string filial)
        {
            switch (filial)
            {
                case "UIA":
                    UIAPointing();
                    break;
                case "UIAII":
                    UIA2Pointing();
                    break;
                default:
                    UIAPointing();
                    break;

            }
            
        }
        
        public void ReportsExpedition(string filial)
        {
            switch (filial)
            {
                case "UIA":
                    UIAExpedition();
                    break;
                case "UIAII":
                    UIA2Expedition();
                    break;
                default:
                    UIAExpedition();
                    break;
            }
            

        }

        public void ReportsQuality(string filial)
        {
            switch (filial)
            {
                case "UIA":
                    UIAQuality();
                    break;
                case "UIAII":
                    UIAQuality();
                    break;
                default:
                    UIAQuality();
                    break;
            }
            
        }

        public void ReportsProduction(string filial)
        {
            switch (filial)
            {
                case "UIA":
                    UIAProduction();
                    break;
                case "UIAII":
                    UIA2Production();
                    break;
                default:
                    UIAProduction();
                    break;
            }
        }

        public void ReportsTickets(string filial)
        {
            switch (filial)
            {
                case "UIA":
                    UIATickets();
                    break;
            }
            
        }

        public void ReportsScheduling(string filial)
        {
            switch (filial)
            {
                case "UIA":
                    UIAScheduling();
                    break;
            }
                
        }

        public void ReportsStops(string filial)
        {
            switch (filial)
            {
                case "UIA":
                    UIAStops();
                    break;
            }
        }

        public void ReportsEggs(string filial)
        {
            switch (filial)
            {
                case "UDM":
                    UDMEggs();
                    break;
            }
            
        }

        public void UIAPointing()
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

        public void UIA2Pointing()
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

        public void UIAExpedition()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "FormaçãoDePalletExpedicao.frx"));
            reportItems.Add(new ReportService(2, "MovimentoCamaraArmazenados.frx"));
            reportItems.Add(new ReportService(21, "MovimentoCamaraExpedidos.frx"));
            reportItems.Add(new ReportService(22, "MovimentoCamaraTODOS.frx"));
            reportItems.Add(new ReportService(23, "MovimentoCamaraTODOS-Matricula.frx"));

        }

        public void UIA2Expedition()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "FormaçãoDePalletExpedicao.frx"));
            reportItems.Add(new ReportService(11, "FormaçãoDePalletExpedicao.frx"));
            reportItems.Add(new ReportService(12, "FormaçãoDePalletExpedicao.frx"));
            reportItems.Add(new ReportService(13, "FormaçãoDePalletExpedicao.frx"));
        }

        public void UIAQuality()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "QualidadeFomentoMensal.frx"));
        }

        public void UIA2Quality()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "QualidadeFomentoMensal.frx"));
        }

        public void UIAProduction()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "MapaMensalProducao.frx"));
            reportItems.Add(new ReportService(11, "ApontamentoRoboCMS.frx"));
        }

        public void UIA2Production()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "MapaMensalProducao.frx"));
            reportItems.Add(new ReportService(11, "ApontamentoRoboCMS.frx"));
        }

        public void UIATickets()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "EtiquetasPallet.frx"));
        }

        public void UIAScheduling()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "AgendamentoSemCargas.frx"));
        }

        public void UIAStops()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "CondenacoesPorMunicipioTODOS.frx"));
            reportItems.Add(new ReportService(11, "CondenacoesPorMunicipio.frx"));
        }

        public void UDMEggs()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "FichaOvos.frx"));
            reportItems.Add(new ReportService(11, "FichaOvos-TODOSDIA.frx"));
        }
    }
}
