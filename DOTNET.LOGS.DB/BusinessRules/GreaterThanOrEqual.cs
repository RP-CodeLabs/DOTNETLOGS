using System;
using System.Collections.Generic;
using System.Linq;
using DOTNET.LOGS.DB.BusinessRules.Interfaces;

namespace DOTNET.LOGS.DB.BusinessRules
{
    internal class GreaterThanOrEqual : IGreaterThanOrEqual
    {
        private readonly IEnumerable<LogDetail> _logDetails;
        public GreaterThanOrEqual(IEnumerable<LogDetail> logDetails) => _logDetails = logDetails;
        IWithExtensionPoint IGreaterThanOrEqual.GreaterThanOrEqual(string startDateTime) => new WithExtensionPoint(_logDetails.Where(l => DateTime.Parse(l.StartDateTime) >= DateTime.Parse(startDateTime)));
    }
}