using System;
using System.Globalization;
using System.Threading.Tasks;
using NUnit.Framework;
using DOTNET.LOGS.DB.Entities;

namespace DOTNET.LOGS.DB.Tests
{
    [TestFixture]
    public class LogInformationProviderTest
    {
        [TestCase("")]
        [TestCase("QA")]
        [TestCase("UAT")]
        [TestCase("REG")]
        public async Task GetLogInformation_ShouldInitialize_BglNetLogContextWithConnectionString(string environment)
        {
            var logQuery = new LogQuery()
            {
                StartDateTime = DateTime.Now.AddDays(-1).ToString(CultureInfo.InvariantCulture),
                EndDateTime = DateTime.Now,
                Application = "Selfservice",
                Level = "Error",
                Environment = environment
            };
            var logInformationProvider = new LogInformationProvider();
            var logData = await logInformationProvider.GetLogInformation(logQuery);
            Assert.IsNotNull(logData);
        }

        [Test]
        public void GetLogInformation_WithOutConnectionString_ShouldReturnLogDataArray()
        {
            var logInformationProvider = new LogInformationProvider();
            var result = logInformationProvider.GetLogInformation(new LogQuery());
            Assert.AreEqual(typeof(LogData[]), result.Result.GetType());
        }

        [TestCase("name=DotNetLogEntities")]
        public void GetLogInformation_EmptyLogQueryObject_ShouldReturnLogDataArray(string environment)
        {
            var logInformationProvider = new LogInformationProvider();
            var result = logInformationProvider.GetLogInformation(new LogQuery());
            Assert.AreEqual(typeof(LogData[]), result.Result.GetType());
        }
    }
}
