using DOTNET.LOGS.DB.BusinessRules.Interfaces;

namespace DOTNET.LOGS.DB.BusinessRules
{
    public class Filter
    {
        private readonly DotNetLogEntities _context;
        public Filter(DotNetLogEntities context) => _context = context;

        public IForExtensionPoint LogDetails() => new ForExtensionPoint(_context.LogDetails);
    }
}