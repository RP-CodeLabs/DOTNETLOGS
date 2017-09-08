using System.Collections.Generic;
using System.Threading.Tasks;
using DOTNET.LOGS.DB.Entities;
using DOTNET.LOGS.Shared;

namespace DOTNET.LOGS.Models
{
    public interface ILogManager
    {
        Task<IReadOnlyList<LogData>> GetLogData(Maybe<LogFilter> logFilter, string environment);
    }
}