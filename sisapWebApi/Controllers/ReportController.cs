using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FastReport.Data;
using FastReport;
using FastReport.Export.PdfSimple;
using FastReport.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sisapWebApi.Models;
using sisapWebApi.Models.ReportModels;
using Newtonsoft.Json;
using sisapWebApi.Services.ReportItems;

namespace sisapWebApi.Controllers
{
    [Route("api/reportgeneration")]
    [ApiController]
    public class ReportController : ControllerBase
    {


        [HttpGet("{id}")]
        // [Authorize]
        public async Task<IActionResult> GetReport(int id, [FromQuery] ReportQuery query)
        {

            // Aqui obtenho a lista de Relatórios
            var reports = new Reports(query.reportModule);
            

            // Aqui crio o tipo do relatório (Sempre PDF)            
            string mime = "application/" + query.Format;
            
            // Aqui faço uma busca pelo relatório na lista de relatórios de acordo com o id dele 
            var reportItem = reports.reportItems.FirstOrDefault((p) => p.Id == id);


            if (reportItem != null)
            {
                // Faz o registro do uso da conexão do FastReport na API
                RegisteredObjects.AddConnection(typeof(FirebirdDataConnection));
                RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
                string reportPath = ("wwwroot/App_Data/" + reportItem.ReportName); // Vejo onde que o arquivo Físico dele está
                MemoryStream stream = new MemoryStream(); // Crio um Stream para ser lido no IFrame do Front
                Report report = new Report();
                Config.WebMode = true;
                // Aqui Carrega o Arquivo
                report.Load(reportPath);
                // Aqui verifica se possui parâmetros
                if (query.Parameter != null)
                    await GetParameters(query, report);
                // Aqui pega e carrega o arquivo com os parametros passados ou não
                report.Prepare();
                // Converte para PDF
                if (query.Format == "pdf")
                {
                    PDFSimpleExport pdf = new PDFSimpleExport();
                    report.Export(pdf, stream);
                }

                ////////////////////////////////////////////////////////////////////
                //Conversão para EXCEL, não está funcionando falta passar o arquivo em pdf ali no new Document
                if (query.Format == "xlsx")
                {
                    PDFSimpleExport pdf = new PDFSimpleExport();
                    // Use the stream to store the report, so as not to create unnecessary files
                    report.Export(pdf, stream);

                    var pdfFile = string.Concat(Path.GetFileNameWithoutExtension(reportPath), ".", "pdf");
                    var excelFile = string.Concat(Path.GetFileNameWithoutExtension(reportPath), ".", query.Format);
                    var document = File(stream.ToArray(), mime, "wwwroot\\App_Data\\"+pdfFile);
                    /// Crio um arquivo PDF temporário
                    System.IO.File.WriteAllBytes("wwwroot\\App_Data\\" + pdfFile, document.FileContents);

                }
                //////////////////////////////////////////////////////////////////////
                // Obtenho o nome do relatório
                var file = string.Concat(Path.GetFileNameWithoutExtension(reportPath), ".", query.Format);

                if (query.Inline)
                    // Aqui sempre vai mostrar o pdf no Frame 
                    return File(stream.ToArray(), mime);
                else
                    // Aqui vai fazer o download do arquivo
                    return File(stream.ToArray(), mime, file); // 
            }
            else
                return NotFound();
        }

        private static async Task GetParameters(ReportQuery query, Report report)
        {

            // Aqui converto o parametro enviado do FRONT do Relatório se houver mapeando os parametros
            var parametros = JsonConvert.DeserializeObject<ReportParameters>(query.Parameter);
            if (parametros.LocalUser != null)
                report.SetParameterValue("LocalUsuario", parametros.LocalUser);
            if (parametros.InitialDate != null)
                report.SetParameterValue("DataInicial", parametros.InitialDate);
            if (parametros.EndDate != null)
                report.SetParameterValue("DataFinal", parametros.EndDate);
            if (parametros.Name != null)
                report.SetParameterValue("Nome", parametros.Name);
            if (parametros.Sector != null)
                report.SetParameterValue("Setor", parametros.Sector);
            if (parametros.Shift != null)
                report.SetParameterValue("Turno", parametros.Shift);
            if (parametros.Period != null)
                report.SetParameterValue("Periodo", parametros.Period);
            if (parametros.Id != null)
                report.SetParameterValue("Id", parametros.Id);
            if (parametros.NrPallet != null)
                report.SetParameterValue("NrPallet", parametros.NrPallet);
            if (parametros.CodSicop != null)
                report.SetParameterValue("CodSicop", parametros.CodSicop);
            if (parametros.Chambers != null)
                report.SetParameterValue("Camaras", parametros.Chambers);
            if (parametros.InitialHour != null)
                report.SetParameterValue("HoraInicial", parametros.InitialHour);
            if (parametros.EndHour != null)
                report.SetParameterValue("HoraFinal", parametros.EndHour);
            if (parametros.Month != null)
                report.SetParameterValue("Mes", parametros.Month);
            if (parametros.Year != null)
                report.SetParameterValue("Ano", parametros.Year);

        }
    }

}
