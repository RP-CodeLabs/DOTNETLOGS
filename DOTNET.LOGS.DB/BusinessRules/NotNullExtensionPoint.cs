using System.Collections.Generic;
using System.Linq;
using DOTNET.LOGS.DB.BusinessRules.Interfaces;

namespace DOTNET.LOGS.DB.BusinessRules
{
    internal class NotNullExtensionPoint : INotNullExtensionPoint
    {
        private readonly IEnumerable<LogDetail> _logDetails;
        public NotNullExtensionPoint(IEnumerable<LogDetail> logDetails) => _logDetails = logDetails;

        public IAndAlsoExtensionPoint NotNull(string startDateTime) => new AndAlsoExtensionPoint(_logDetails.Where(l => !string.IsNullOrEmpty(l.StartDateTime)));
    }
}