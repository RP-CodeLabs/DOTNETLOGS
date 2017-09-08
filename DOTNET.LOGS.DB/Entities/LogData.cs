using System;

namespace DOTNET.LOGS.DB.Entities
{
    public class LogData
    {
        public string CustomerSessionId { get; set; }
        public string StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string CurrentUrl { get; set; }
        public string Exception { get; set; }
        public string UrlReferrrer { get; set; }
    }
}