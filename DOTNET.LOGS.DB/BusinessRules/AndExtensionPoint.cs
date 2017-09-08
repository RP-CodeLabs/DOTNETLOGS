using System.Collections.Generic;
using DOTNET.LOGS.DB.BusinessRules.Interfaces;

namespace DOTNET.LOGS.DB.BusinessRules
{
    public class AndExtensionPoint : IAndExtensionPoint
    {
        private readonly IEnumerable<LogDetail> _logDetails;
        public AndExtensionPoint(IEnumerable<LogDetail> logDetails) => _logDetails = logDetails;

        IStartDateTime IAndExtensionPoint.And() => new StartDateTime(_logDetails);
    }
}