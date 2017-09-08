namespace DOTNET.LOGS.DB.BusinessRules.Interfaces
{
    public interface INotNullExtensionPoint
    {
        IAndAlsoExtensionPoint NotNull(string startDateTime);
    }
}