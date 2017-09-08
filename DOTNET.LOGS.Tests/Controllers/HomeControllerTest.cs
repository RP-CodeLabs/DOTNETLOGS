using System.Threading.Tasks;
using System.Web.Mvc;
using DOTNET.LOGS.Controllers;
using DOTNET.LOGS.Models;
using NUnit.Framework;

namespace DOTNET.LOGS.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Index()
        {
            var controller = new HomeController();
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Logs_ModelState_False()
        {
            var contoller = new HomeController();
            contoller.ModelState.AddModelError("key", "Testing invalid ModelState");
            var result = await contoller.Logs(new LogFilter()) as ViewResult;
            Assert.True(result != null && result.Model.GetType() == typeof(LogFilter));
        }
    }
}
