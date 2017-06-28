using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Metro/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Metro/Details/5

        public ActionResult UI()
        {
            return View();
        }

        public ActionResult Forms()
        {
            return View();
        }

        public ActionResult Charts()
        {
            return View();
        }

        public ActionResult Tables()
        {
            return View();
        }
    }
}
