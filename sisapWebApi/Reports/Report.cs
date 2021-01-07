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
            reportItems.Add(new ReportService(1, "Pointing/ApontamentoDeProducao.frx"));
            reportItems.Add(new ReportService(11, "Pointing/ApontamentoDeProducaoTODOS.frx"));
            reportItems.Add(new ReportService(12, "Pointing/ApontamentoDeProducaoTURNO-Periodo.frx"));
            reportItems.Add(new ReportService(13, "Pointing/ApontamentoDeProducaoPERIODO-Turno.frx"));
            reportItems.Add(new ReportService(14, "Pointing/ApontamentoDeProducaoUIC-Turno.frx"));
            reportItems.Add(new ReportService(15, "Pointing/ApontamentoDeProducaoUIC.frx"));
            reportItems.Add(new ReportService(16, "Pointing/ApontamentoDeRefeitorio.frx"));
        }

        public void UIA2Pointing()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "Pointing/ApontamentoDeProducao.frx"));
            reportItems.Add(new ReportService(11, "Pointing/ApontamentoDeProducaoTODOS.frx"));
            reportItems.Add(new ReportService(12, "Pointing/ApontamentoDeProducaoTURNO-Periodo.frx"));
            reportItems.Add(new ReportService(13, "Pointing/ApontamentoDeProducaoPERIODO-Turno.frx"));
            reportItems.Add(new ReportService(14, "Pointing/ApontamentoDeProducaoUIC-Turno.frx"));
            reportItems.Add(new ReportService(15, "Pointing/ApontamentoDeProducaoUIC.frx"));
            reportItems.Add(new ReportService(16, "Pointing/ApontamentoDeRefeitorio.frx"));
        }

        public void UIAExpedition()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "Expedition/FormaçãoDePalletExpedicao.frx"));
            reportItems.Add(new ReportService(2, "Expedition/MovimentoOperadorArmazenados.frx"));
            reportItems.Add(new ReportService(21, "Expedition/MovimentoOperadorExpedidos.frx"));
            reportItems.Add(new ReportService(22, "Expedition/MovimentoCamaraTODOS.frx"));
            reportItems.Add(new ReportService(23, "Expedition/MovimentoOperadorTODOS-Matricula.frx"));
            reportItems.Add(new ReportService(24, "Expedition/MovimentoCamaraArmazenados-SemMatriculaTurno1.frx"));
            reportItems.Add(new ReportService(25, "Expedition/MovimentoCamaraExpedidos-SemMatriculaTurno1.frx"));
            reportItems.Add(new ReportService(26, "Expedition/MovimentoCamaraArmazenados-SemMatriculaTodos.frx"));
            reportItems.Add(new ReportService(27, "Expedition/MovimentoCamaraExpedidos-SemMatriculaTodos.frx"));
            reportItems.Add(new ReportService(28, "Expedition/MovimentoOperadorArmazenadosSemMatricula.frx"));
            reportItems.Add(new ReportService(29, "Expedition/MovimentoOperadorExpedidosSemMatricula.frx"));
            reportItems.Add(new ReportService(3, "Expedition/MovimentoOperadorTodosArmazenadosExpedidos.frx"));
            reportItems.Add(new ReportService(31, "Expedition/MovimentoOperadorTodosArmazenadosExpedidosTurno1.frx"));
            reportItems.Add(new ReportService(32, "Expedition/MovimentoOperadorTodosArmazenadosExpedidosTurno2.frx"));
            reportItems.Add(new ReportService(33, "Expedition/MovimentoOperadorTodosArmazenadosExpedidosTurno3.frx"));
            reportItems.Add(new ReportService(34, "Expedition/MovimentoCamaraTODOSTurno1.frx"));
            reportItems.Add(new ReportService(35, "Expedition/MovimentoCamaraTODOSTurno2.frx"));
            reportItems.Add(new ReportService(36, "Expedition/MovimentoCamaraTODOSTurno3.frx"));
            reportItems.Add(new ReportService(37, "Expedition/MovimentoCamaraArmazenados-SemMatriculaTurno2.frx"));
            reportItems.Add(new ReportService(38, "Expedition/MovimentoCamaraArmazenados-SemMatriculaTurno3.frx"));
            reportItems.Add(new ReportService(39, "Expedition/MovimentoCamaraExpedidos-SemMatriculaTurno2.frx"));
            reportItems.Add(new ReportService(4, "Expedition/MovimentoCamaraExpedidos-SemMatriculaTurno3.frx"));
            reportItems.Add(new ReportService(41, "Expedition/MapaCamara.frx"));
            reportItems.Add(new ReportService(42, "Expedition/MapaCamara.frx"));
        }

        public void UIA2Expedition()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "Expedition/FormaçãoDePalletExpedicao.frx"));
            reportItems.Add(new ReportService(11, "Expedition/FormaçãoDePalletExpedicao.frx"));
            reportItems.Add(new ReportService(12, "Expedition/FormaçãoDePalletExpedicao.frx"));
            reportItems.Add(new ReportService(13, "Expedition/FormaçãoDePalletExpedicao.frx"));
        }

        public void UIAQuality()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "Quality/QualidadeFomentoMensal.frx"));
        }

        public void UIA2Quality()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "Quality/QualidadeFomentoMensal.frx"));
        }

        public void UIAProduction()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "Production/MapaMensalProducao.frx"));
            reportItems.Add(new ReportService(11, "Production/ApontamentoRoboCMS.frx"));
        }

        public void UIA2Production()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "Production/MapaMensalProducao.frx"));
            reportItems.Add(new ReportService(11, "Production/ApontamentoRoboCMS.frx"));
        }

        public void UIATickets()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "Tickets/EtiquetasPallet.frx"));
        }

        public void UIAScheduling()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "Scheduling/AgendamentoSemCargas.frx"));
        }

        public void UIAStops()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "Stops/CondenacoesPorMunicipioTODOS.frx"));
            reportItems.Add(new ReportService(11, "Stops/CondenacoesPorMunicipio.frx"));
        }

        public void UDMEggs()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "Eggs/FichaOvos.frx"));
            reportItems.Add(new ReportService(11, "Eggs/FichaOvos-TODOSDIA.frx"));
        }
    }
}
