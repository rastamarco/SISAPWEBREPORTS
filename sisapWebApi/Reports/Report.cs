﻿using sisapWebApi.Reports;
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
                    ReportsApontamento();
                    break;
                case 2:
                    ReportsExpedicao();
                    break;
                case 3:
                    ReportsQualidade();
                    break;
                case 4:
                    ReportsProducao();
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
            reportItems.Add(new ReportService(16, "ApontamentoDeRefeitorio.frx"));
        }
        
        public void ReportsExpedicao()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(2, "FormaçãoDePalletExpedicao.frx"));
        }

        public void ReportsQualidade()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(3, "QualidadeFomentoMensal.frx"));
        }

        public void ReportsProducao()
        {
            reportItems = new List<ReportService>();
            reportItems.Add(new ReportService(4, "MapaMensalProducao.frx"));
        }
    }
}