namespace DOTNET.LOGS.DB.BusinessRules.Interfaces
{
    public interface IErrorExtensionPoint
    {
        IAndExtensionPoint Error(string level);
    }
}