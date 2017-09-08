using System.Collections.Generic;
using System.Threading.Tasks;
using DOTNET.LOGS.DB.Entities;
using DOTNET.LOGS.Shared;

namespace DOTNET.LOGS.DB
{
    public interface ILogInformationProvider
    {
        Task<IReadOnlyList<LogData>> GetLogInformation(Maybe<LogQuery> logQuery);
    }
}