using System;

namespace DOTNET.LOGS.DB.Entities
{
    public class LogQuery
    {
        public string StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Application { get; set; }
        public string Level { get; set; }
        public string Environment { get; set; }
    }
}