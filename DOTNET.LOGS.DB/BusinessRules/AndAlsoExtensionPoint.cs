using System.Collections.Generic;
using DOTNET.LOGS.DB.BusinessRules.Interfaces;

namespace DOTNET.LOGS.DB.BusinessRules
{
    internal class AndAlsoExtensionPoint : IAndAlsoExtensionPoint
    {
        private readonly IEnumerable<LogDetail> _logDetails;
        public AndAlsoExtensionPoint(IEnumerable<LogDetail> logDetailses) => _logDetails = logDetailses;

        public IGreaterThanOrEqual AndAlso() => new GreaterThanOrEqual(_logDetails);
    }
}