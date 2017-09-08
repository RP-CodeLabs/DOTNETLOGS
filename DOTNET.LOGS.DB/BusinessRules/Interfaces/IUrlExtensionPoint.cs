using System.Collections.Generic;

namespace DOTNET.LOGS.DB.BusinessRules.Interfaces
{
    public interface IUrlExtensionPoint
    {
        IEnumerable<LogDetail> Contains(string application);
    }
}