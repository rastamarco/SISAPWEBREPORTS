using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sisapWebApi.Services
{
    public class ReportService
    {
        public ReportService(int Id, string ReportName)
        {
            this.Id = Id;
            this.ReportName = ReportName;
        }
        // Report ID
        public int Id { get; set; }
        // Report File Name
        public string ReportName { get; set; }
    }
}
