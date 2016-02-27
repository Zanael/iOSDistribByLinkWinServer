using System.Web.Mvc;

namespace iPhoneUDID.Controllers
{
    public class ThanksController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Thanks Page";

            return View();
        }
    }
}