using System;
using System.Collections.Generic;
using System.Linq;
using DOTNET.LOGS.DB.BusinessRules.Interfaces;

namespace DOTNET.LOGS.DB.BusinessRules
{
    public class EndDateTime : IEndDateTime
    {
        private IEnumerable<LogDetail> _logDetails;
        public EndDateTime(IEnumerable<LogDetail> logDetails) => _logDetails = logDetails;

        ILessThanOrEqual IEndDateTime.EndDateTime() => new LessThanOrEqual(_logDetails);

        public IEndDateTime LessThanOrEqualTo(DateTime endDateTime)
        {
            _logDetails = _logDetails.Where(l => l.EndDateTime <= endDateTime);
            return this;
        }

        public IUrlExtensionPoint And() => new UrlExtensionPoint(_logDetails);
    }
}