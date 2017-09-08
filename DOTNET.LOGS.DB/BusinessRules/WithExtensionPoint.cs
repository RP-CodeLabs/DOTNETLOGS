using System.Collections.Generic;
using DOTNET.LOGS.DB.BusinessRules.Interfaces;

namespace DOTNET.LOGS.DB.BusinessRules
{
    internal class WithExtensionPoint : IWithExtensionPoint
    {
        private readonly IEnumerable<LogDetail> _logDetails;

        public WithExtensionPoint(IEnumerable<LogDetail> logDetails) => _logDetails = logDetails;

        public IEndDateTime With() => new EndDateTime(_logDetails);
    }
}