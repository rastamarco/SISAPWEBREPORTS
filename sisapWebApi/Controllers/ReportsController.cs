//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using FastReport;
//using FastReport.Export.Image;
//using FastReport.Export.Html;
//using FastReport.Export.Pdf;
//using FastReport.Utils;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using sisapWebApi.Models;
//using sisapWebApi.Repositories;
//using System.Runtime.CompilerServices;

//namespace sisapWebApi.Controllers
//{
//    [Route("api/reports")]
//    [ApiController]
//    public class ReportsController : ControllerBase
//    {
//        ArrayList<ReportRepository> reportItems;
//        public ReportsController()
//        {
//            reportItems = new ArrayList<ReportRepository>();
//            ReportRepository Teste = new ReportRepository(Id = 1, ReportName = "Teste.frx");
//            reportItems.add(Teste);
//        }

//    //Attribute has required id parameter
//    [HttpGet("{id}")]
//        public async Tasks<IActionResult> Get(int id, [FromQuery] ReportQuery query)
//        {
//            // MIME header with default value
//            string mime = "application/" + query.Format;
//            ReportRepository reportItem = reportItems.FirstOrDefault((p) => p.Id == id);
//            if (reportItem != null)
//            {
//                //string webRootPath = _hostingEnvironment.WebRootPath; // determine the path to the wwwroot folder
//                string webRootPath = "reportsroot";
//                string reportPath = (webRootPath + "/App_Data/" + reportItem.ReportName); // determine the path to the report
//                using (MemoryStream stream = new MemoryStream()) // Create a stream for the report
//                {
//                    try
//                    {
//                        using (DataSet dataSet = new DataSet())
//                        {
//                            // Fill the source by data
//                            // dataSet.ReadXml(dataPath);
//                            // Turn on web mode FastReport
//                            Config.WebMode = true;
//                            using (Report report = new Report())
//                            {
//                                report.Load(reportPath);
//                                report.RegisterData(dataSet, "NorthWind");
//                                if (query.Parameter != null)
//                                {
//                                    report.SetParameterValue("Parameter", query.Parameter);
//                                }
//                                //Prepare the report
//                                report.Prepare();

//                                if (query.Format == "png")
//                                {
//                                    // Export report to PDF
//                                    ImageExport png = new ImageExport();
//                                    png.ImageFormat = ImageExportFormat.Png;
//                                    png.SeparateFiles = false;
//                                    // Use the stream to store the report, so as not to create unnecessary files
//                                    report.Export(png, stream);
//                                }
//                                //If html report format is selected
//                                else if (query.Format == "html")
//                                {
//                                    // Export Report to HTML
//                                    HTMLExport html = new HTMLExport();
//                                    html.SinglePage = true; // Single page report
//                                    html.Navigator = false; // Top navigation bar
//                                    html.EmbedPictures = true; // Embeds images into a document
//                                    report.Export(html, stream);
//                                    mime = "text/" + query.Format; // Override mime for html
//                                }
//                            }
//                        }
//                        // Get the name of the resulting report file with the necessary extension 
//                        var file = string.Concat(Path.GetFileNameWithoutExtension(reportPath), ".", query.Format);
//                        // If the inline parameter is true, then open the report in the browser
//                        if (query.Inline)
//                            return File(stream.ToArray(), mime);
//                        else
//                            // Otherwise download the report file 
//                            return File(stream.ToArray(), mime, file); // attachment
//                    }
//                    // Handle exceptions
//                    catch (Exception ex)
//                    {
//                        return new NoContentResult();
//                    }
//                    finally
//                    {
//                        stream.Dispose();
//                    }
//                }
//            }
//            else
//                return NotFound();
//        }
//    }
//}