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
                case "UIAIII":
                    UIA3Pointing();
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
            reportItems.Add(new ReportService(1, "Pointing/UIA/ApontamentoDeProducao.frx"));
            reportItems.Add(new ReportService(11, "Pointing/UIA/ApontamentoDeProducaoTODOS.frx"));
            reportItems.Add(new ReportService(12, "Pointing/UIA/ApontamentoDeProducaoTURNO-Periodo.frx"));
            reportItems.Add(new ReportService(13, "Pointing/UIA/ApontamentoDeProducaoPERIODO-Turno.frx"));
            reportItems.Add(new ReportService(14, "Pointing/UIA/ApontamentoDeProducaoUIC-Turno.frx"));
            reportItems.Add(new ReportService(15, "Pointing/UIA/ApontamentoDeProducaoUIC.frx"));
            reportItems.Add(new ReportService(16, "Pointing/UIA/ApontamentoDeRefeitorio.frx"));
        }

        public void UIA2Pointing()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "Pointing/UIA2/ApontamentoDeProducao.frx"));
            reportItems.Add(new ReportService(11, "Pointing/UIA2/ApontamentoDeProducaoTODOS.frx"));
            reportItems.Add(new ReportService(12, "Pointing/UIA2/ApontamentoDeProducaoTURNO-Periodo.frx"));
            reportItems.Add(new ReportService(13, "Pointing/UIA2/ApontamentoDeProducaoPERIODO-Turno.frx"));
        }

        public void UIA3Pointing()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "Pointing/UIA3/ApontamentoDeProducao.frx"));
            reportItems.Add(new ReportService(11, "Pointing/UIA3/ApontamentoDeProducaoTODOS.frx"));
            reportItems.Add(new ReportService(12, "Pointing/UIA3/ApontamentoDeProducaoTURNO-Periodo.frx"));
            reportItems.Add(new ReportService(13, "Pointing/UIA3/ApontamentoDeProducaoPERIODO-Turno.frx"));
        }

        public void UIAExpedition()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "Expedition/UIA/FormaçãoDePalletExpedicao.frx"));
            reportItems.Add(new ReportService(2, "Expedition/UIA/MovimentoOperadorArmazenados.frx"));
            reportItems.Add(new ReportService(21, "Expedition/UIA/MovimentoOperadorExpedidos.frx"));
            reportItems.Add(new ReportService(22, "Expedition/UIA/MovimentoCamaraTODOS.frx"));
            reportItems.Add(new ReportService(23, "Expedition/UIA/MovimentoOperadorTODOS-Matricula.frx"));
            reportItems.Add(new ReportService(24, "Expedition/UIA/MovimentoCamaraArmazenados-SemMatriculaTurno1.frx"));
            reportItems.Add(new ReportService(25, "Expedition/UIA/MovimentoCamaraExpedidos-SemMatriculaTurno1.frx"));
            reportItems.Add(new ReportService(26, "Expedition/UIA/MovimentoCamaraArmazenados-SemMatriculaTodos.frx"));
            reportItems.Add(new ReportService(27, "Expedition/UIA/MovimentoCamaraExpedidos-SemMatriculaTodos.frx"));
            reportItems.Add(new ReportService(28, "Expedition/UIA/MovimentoOperadorArmazenadosSemMatricula.frx"));
            reportItems.Add(new ReportService(29, "Expedition/UIA/MovimentoOperadorExpedidosSemMatricula.frx"));
            reportItems.Add(new ReportService(3, "Expedition/UIA/MovimentoOperadorTodosArmazenadosExpedidos.frx"));
            reportItems.Add(new ReportService(31, "Expedition/UIA/MovimentoOperadorTodosArmazenadosExpedidosTurno1.frx"));
            reportItems.Add(new ReportService(32, "Expedition/UIA/MovimentoOperadorTodosArmazenadosExpedidosTurno2.frx"));
            reportItems.Add(new ReportService(33, "Expedition/UIA/MovimentoOperadorTodosArmazenadosExpedidosTurno3.frx"));
            reportItems.Add(new ReportService(34, "Expedition/UIA/MovimentoCamaraTODOSTurno1.frx"));
            reportItems.Add(new ReportService(35, "Expedition/UIA/MovimentoCamaraTODOSTurno2.frx"));
            reportItems.Add(new ReportService(36, "Expedition/UIA/MovimentoCamaraTODOSTurno3.frx"));
            reportItems.Add(new ReportService(37, "Expedition/UIA/MovimentoCamaraArmazenados-SemMatriculaTurno2.frx"));
            reportItems.Add(new ReportService(38, "Expedition/UIA/MovimentoCamaraArmazenados-SemMatriculaTurno3.frx"));
            reportItems.Add(new ReportService(39, "Expedition/UIA/MovimentoCamaraExpedidos-SemMatriculaTurno2.frx"));
            reportItems.Add(new ReportService(4, "Expedition/UIA/MovimentoCamaraExpedidos-SemMatriculaTurno3.frx"));
            reportItems.Add(new ReportService(41, "Expedition/UIA/MapaCamara.frx"));
            reportItems.Add(new ReportService(42, "Expedition/UIA/LocalizacaoProdutosComProduto.frx"));
            reportItems.Add(new ReportService(43, "Expedition/UIA/LocalizacaoProdutos.frx"));
            reportItems.Add(new ReportService(44, "Expedition/UIA/LocalizacaoProdutosRuasVazias.frx"));
            reportItems.Add(new ReportService(45, "Expedition/UIA/HistoricoCamaraPalletArmazenados.frx"));
            reportItems.Add(new ReportService(46, "Expedition/UIA/HistoricoCamaraPalletExpedidos.frx"));
            reportItems.Add(new ReportService(47, "Expedition/UIA/HistoricoCamaraPalletExcluidos.frx"));
            reportItems.Add(new ReportService(48, "Expedition/UIA/HistoricoCamaraPalletArmazenadosProduto.frx"));
            reportItems.Add(new ReportService(49, "Expedition/UIA/HistoricoCamaraPalletExpedidosProduto.frx"));
            reportItems.Add(new ReportService(50, "Expedition/UIA/HistoricoCamaraPalletExcluidosProduto.frx"));
            reportItems.Add(new ReportService(51, "Expedition/UIA/PosicoesVaziasNasCamaras.frx"));
            reportItems.Add(new ReportService(52, "Expedition/UIA/PesoProdutoCamara.frx"));
            reportItems.Add(new ReportService(53, "Expedition/UIA/ProdutosEmbarques.frx"));
            reportItems.Add(new ReportService(54, "Expedition/UIA/EmbarquesPeriodos.frx"));
            reportItems.Add(new ReportService(55, "Expedition/UIA/EmbarquesPeriodosConferente.frx"));
            reportItems.Add(new ReportService(56, "Expedition/UIA/EmbarquesParadasDesembarques.frx"));
            reportItems.Add(new ReportService(57, "Expedition/UIA/EmbarquesParadasDesembarquesTurno.frx"));
            reportItems.Add(new ReportService(58, "Expedition/UIA/EmbarquesParadasDesembarquesTurnoMatricula.frx"));
            reportItems.Add(new ReportService(59, "Expedition/UIA/EmbarquesParadasDesembarquesMatricula.frx"));
            reportItems.Add(new ReportService(60, "Expedition/UIA/EntradaSaidaTunelCongelamento.frx"));
            reportItems.Add(new ReportService(61, "Expedition/UIA/CargasEmbarcadas.frx"));
            reportItems.Add(new ReportService(62, "Expedition/UIA/ReagendamentoDeCargas.frx"));
            reportItems.Add(new ReportService(63, "Expedition/UIA/ReagendamentoDeCargasEspecificos.frx"));
            reportItems.Add(new ReportService(64, "Expedition/UIA/PalletsNaCamaraC.frx"));
            reportItems.Add(new ReportService(65, "Expedition/UIA/PaisesDestinos.frx"));
            reportItems.Add(new ReportService(66, "Expedition/UIA/ProdutosPorSif.frx"));
            reportItems.Add(new ReportService(67, "Expedition/UIA/ProdutosPorSifNr.frx"));
            reportItems.Add(new ReportService(68, "Expedition/UIA/EntradaSaidaTunelCongelamentoTodos.frx"));
            reportItems.Add(new ReportService(69, "Expedition/UIA/EntradaSaidaTunelCongelamentoTurno.frx"));
        }

        public void UIA2Expedition()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "Expedition/UIA2/ProducoesSemIntegracao.frx"));
            reportItems.Add(new ReportService(2, "Expedition/UIA2/ProducoesSemIntegracaoTurnos.frx"));
            reportItems.Add(new ReportService(3, "Expedition/UIA2/ReagendamentoDeCargas.frx"));
            reportItems.Add(new ReportService(4, "Expedition/UIA2/ReagendamentoDeCargasEspecificos.frx"));

        }

        public void UIAQuality()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "Quality/UIA/QualidadeFomentoMensal.frx"));
        }

        public void UIA2Quality()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(1, "Quality/UIA2/QualidadeFomentoMensal.frx"));
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
            reportItems.Add(new ReportService(1, "Eggs/ApontamentoDeProducao.frx"));
            reportItems.Add(new ReportService(11, "Eggs/ApontamentoDeProducaoTODOS.frx"));
            reportItems.Add(new ReportService(12, "Eggs/ApontamentoDeProducaoTURNO-Periodo.frx"));
            reportItems.Add(new ReportService(13, "Eggs/ApontamentoDeProducaoPERIODO-Turno.frx"));
            reportItems.Add(new ReportService(14, "Eggs/FichaOvos.frx"));
            reportItems.Add(new ReportService(15, "Eggs/FichaOvos-TODOSDIA.frx"));
        }
    }
}
