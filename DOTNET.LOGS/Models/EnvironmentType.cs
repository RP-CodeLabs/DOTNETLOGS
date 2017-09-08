using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DOTNET.LOGS.DB.Entities;
using DOTNET.LOGS.Shared;

namespace DOTNET.LOGS.Models
{
    public class EnvironmentType : CustomEnum<EnvironmentType>
    {
        public Func<LogFilter, Task<IReadOnlyList<LogData>>> LogFilter { get; }

        private static readonly ILogManager Manager = DependencyResolver.Current.GetService<ILogManager>();
        
        public static EnvironmentType None = new EnvironmentType(0,"None", null);

        public static EnvironmentType Int = new EnvironmentType(1, "INT", (logFilter) => Manager.GetLogData(logFilter, Int.Description));

        public static EnvironmentType Qa = new EnvironmentType(2, "QA", (logFilter) =>  Manager.GetLogData(logFilter, Qa.Description));

        public static EnvironmentType Uat = new EnvironmentType(3, "UAT", (logFilter) => Manager.GetLogData(logFilter, Uat.Description));

        public static EnvironmentType Reg = new EnvironmentType(4, "REG", (logFilter) => Manager.GetLogData(logFilter, Reg.Description));

        public static EnvironmentType PreProd = new EnvironmentType(5, "PREPROD", (logFilter) => Manager.GetLogData(logFilter, PreProd.Description));

        public static EnvironmentType LiveBretton = new EnvironmentType(6, "LIVE_BRE", (logFilter) => Manager.GetLogData(logFilter, LiveBretton.Description));

        public static EnvironmentType LivePegasus = new EnvironmentType(7, "LIVE_PEG", (logFilter) => Manager.GetLogData(logFilter, LivePegasus.Description));


        public EnvironmentType(int key, string description, Func<LogFilter, Task<IReadOnlyList<LogData>>> logFilter)
        {
            LogFilter = logFilter;
            Key = key.ToString();
            Add(key.ToString(), description, this);
        }
    }
}