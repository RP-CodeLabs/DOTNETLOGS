using System;
using System.Collections.Generic;
using System.Linq;
using DOTNET.LOGS.DB.BusinessRules.Interfaces;

namespace DOTNET.LOGS.DB.BusinessRules
{
    internal class LessThanOrEqual : ILessThanOrEqual
    {
        private readonly IEnumerable<LogDetail> _logDetails;

        public LessThanOrEqual(IEnumerable<LogDetail> logDetails) => _logDetails = logDetails;

        IAlsoExtensionPoint ILessThanOrEqual.LessThanOrEqual(DateTime endDateTime) => new AlsoExtensionPoint(_logDetails.Where(l => l.EndDateTime <= endDateTime));
    }
}