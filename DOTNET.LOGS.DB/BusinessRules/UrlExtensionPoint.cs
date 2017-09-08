using System.Collections.Generic;
using System.Linq;
using DOTNET.LOGS.DB.BusinessRules.Interfaces;

namespace DOTNET.LOGS.DB.BusinessRules
{
    public class UrlExtensionPoint : IUrlExtensionPoint
    {
        private readonly IEnumerable<LogDetail> _logDetails;
        public UrlExtensionPoint(IEnumerable<LogDetail> logDetails) => _logDetails = logDetails;

        public IEnumerable<LogDetail> Contains(string application) => _logDetails.Where(l => l.Url.Contains(application));
    }
}