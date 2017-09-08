using System.Collections.Generic;
using System.Linq;
using DOTNET.LOGS.DB.BusinessRules.Interfaces;

namespace DOTNET.LOGS.DB.BusinessRules
{
    internal class AlsoExtensionPoint : IAlsoExtensionPoint
    {
        private readonly IEnumerable<LogDetail> _logDetails;

        public AlsoExtensionPoint(IEnumerable<LogDetail> logDetails) => _logDetails = logDetails;

        public IEnumerable<LogDetail> Contains(string application) => _logDetails.Where(l => l.Url.Contains(application));
    }
}