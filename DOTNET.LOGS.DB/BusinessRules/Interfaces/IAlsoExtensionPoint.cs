using System.Collections.Generic;

namespace DOTNET.LOGS.DB.BusinessRules.Interfaces
{
    public interface IAlsoExtensionPoint
    {
        IEnumerable<LogDetail> Contains(string application);
    }
}