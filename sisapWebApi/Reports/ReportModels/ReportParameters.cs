using System.Collections.Generic;

namespace sisapWebApi.Reports.ReportModels
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
        public string InitialHour { get; set; }
        public string EndHour { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Line { get; set; }
        public string IdProvider { get; set; }
        public string ProviderName { get; set; }
        public string TicketType { get; set; }
        public string NrPlaca { get; set; }
        public string NrNote { get; set; }
        public string City { get; set; }
    }
}
