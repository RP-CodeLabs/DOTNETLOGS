using System.Collections.Generic;
using DOTNET.LOGS.DB.BusinessRules.Interfaces;

namespace DOTNET.LOGS.DB.BusinessRules
{
    public class StartDateTime : IStartDateTime
    {
        private readonly IEnumerable<LogDetail> _logDetails;
        public StartDateTime(IEnumerable<LogDetail> logDetails) => _logDetails = logDetails;

        INotNullExtensionPoint IStartDateTime.StartDateTime() => new NotNullExtensionPoint(_logDetails);
    }
}