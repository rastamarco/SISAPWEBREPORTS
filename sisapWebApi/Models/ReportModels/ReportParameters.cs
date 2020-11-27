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
        public string Shift { get; set; }
        public string Period { get; set; }
        public string Sector { get; set; }
        public string Name { get; set; }
        public string LocalUser { get; set; }
        public string Id { get; set; }
        public string NrPallet { get; set; }
        public string CodSicop { get; set; }
        public List<string> Chambers { get; set; }
    }
}
