using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sisapWebApi.Models.ReportModels
{
    public class ReportParameters
    {
        public string InitialDate { get; set; }
        public string EndDate { get; set; }
        public int Shift { get; set; }
        public int Period { get; set; }
        public int Sector { get; set; }
        public string Name { get; set; }
        public string LocalUser { get; set; }
        public int Id { get; set; }
    }
}
