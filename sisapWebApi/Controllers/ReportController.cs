using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FastReport.Data;
using FastReport;
using FastReport.Web;
using FastReport.Export.Image;
using FastReport.Export.PdfSimple;
using FastReport.Utils;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sisapWebApi.Models;
using sisapWebApi.Repositories;

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
            //////////// Veriricar jeito para iniciar com todos os relatórios no ínicio da API
            List<ReportRepository> reportItems;
            reportItems = new List<ReportRepository>();
            reportItems.Add(new ReportRepository(1, "Teste2.frx"));
            ///
            ///
            // 
            string mime = "application/" + query.Format;
            ReportRepository reportItem = reportItems.FirstOrDefault((p) => p.Id == id);
            if (reportItem != null)
            {
                RegisteredObjects.AddConnection(typeof(FirebirdDataConnection));
                string reportPath = ("wwwroot/App_Data/" + reportItem.ReportName); // determine the path to the report
                MemoryStream stream = new MemoryStream(); // Create a stream for the report
                Report report = new Report();
                Config.WebMode = true;
                report.Load(reportPath);
                report.Prepare();

                if (query.Format == "pdf")
                {
                    // Export report to PDF
                    PDFSimpleExport pdf = new PDFSimpleExport();
                    // Use the stream to store the report, so as not to create unnecessary files
                    report.Export(pdf, stream);
                }

                if (query.Format == "xlsx")
                {
                    // Export report to PDF
                    // PDFSimpleExport pdf = new PDFSimpleExport();

                    // Use the stream to store the report, so as not to create unnecessary files
                    //report.Export(pdf, stream);
                }

                // Get the name of the resulting report file with the necessary extension
                var file = string.Concat(Path.GetFileNameWithoutExtension(reportPath), ".", query.Format);
                //// If the inline parameter is true, then open the report in the browser
                if (query.Inline)
                    return File(stream.ToArray(), mime);
                else
                    // Otherwise download the report file 
                    return File(stream.ToArray(), mime, file); // 
            }
            else
                return NotFound();
        }

    }

}
