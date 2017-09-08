using System;

namespace DOTNET.LOGS.DB.BusinessRules.Interfaces
{
    public interface ILessThanOrEqual
    {
        IAlsoExtensionPoint LessThanOrEqual(DateTime endDateTime);
    }
}