using System.Data.Entity;
using System.Linq;
using DOTNET.LOGS.DB.BusinessRules.Interfaces;

namespace DOTNET.LOGS.DB.BusinessRules
{
    public class ErrorExtensionPoint : IErrorExtensionPoint
    {
        private readonly IDbSet<LogDetail> _logDetails;
        public ErrorExtensionPoint(IDbSet<LogDetail> logDetails) => _logDetails = logDetails;

        public IAndExtensionPoint Error(string level) => new AndExtensionPoint(_logDetails.Where(l => l.Level == level));
    }
}