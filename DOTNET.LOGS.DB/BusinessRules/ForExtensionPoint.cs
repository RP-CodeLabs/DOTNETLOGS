using System.Data.Entity;
using DOTNET.LOGS.DB.BusinessRules.Interfaces;

namespace DOTNET.LOGS.DB.BusinessRules
{
    public class ForExtensionPoint : IForExtensionPoint
    {
        private readonly IDbSet<LogDetail> _logDetails;
        public ForExtensionPoint(IDbSet<LogDetail> logDetails) => _logDetails = logDetails;

        public IErrorExtensionPoint For() => new ErrorExtensionPoint(_logDetails);
    }
}