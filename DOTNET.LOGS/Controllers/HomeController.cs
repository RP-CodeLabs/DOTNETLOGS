using System.Threading.Tasks;
using System.Web.Mvc;
using DOTNET.LOGS.Models;

namespace DOTNET.LOGS.Controllers
{
    public class HomeController : Controller
    {
        [AcceptVerbs("GET")]
        public ActionResult Index()
        {
            return View(new LogFilter());
        }

        [AcceptVerbs("POST")]
        public async Task<ActionResult> Logs(LogFilter model)
        {
            return ModelState.IsValid ? View(await EnvironmentType.GetFor(model.UserSelectedEnvironment).LogFilter(model)) : View("Index", model);
        }
    }
}