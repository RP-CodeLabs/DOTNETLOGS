using System.Collections.Generic;
using DOTNET.LOGS.Models;
using DOTNET.LOGS.DB;
using DOTNET.LOGS.DB.Entities;
using Moq;
using NUnit.Framework;

namespace DOTNET.LOGS.Tests
{
    [TestFixture]
    public class LogManagerTest
    {
        private ILogInformationProvider _logInformationProvider;
        private ILogManager _logManager;

        [OneTimeSetUp]
        public void Setup()
        {
            _logInformationProvider = new LogInformationProvider();
            _logManager = new LogManager(_logInformationProvider);
        }

        [Test]
        public void GetLogData_With_LogFilter_As_Null_Should_Return_LogData()
        {
            var result = _logManager.GetLogData(null, "");
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetLogData_Verify_GetLogInformation_Is_Called_AtLeastOnce()
        {
            var logInformationProvider = Mock.Of<ILogInformationProvider>();
            Mock.Get(logInformationProvider).Setup(x=>x.GetLogInformation(It.IsAny<LogQuery>())).ReturnsAsync(new List<LogData>());
            var logManager = new LogManager(logInformationProvider);
            var result = logManager.GetLogData(new LogFilter(), "");
            Mock.Get(logInformationProvider).Verify(x=>x.GetLogInformation(It.IsAny<LogQuery>()), Times.Once());
        }
    }
}
