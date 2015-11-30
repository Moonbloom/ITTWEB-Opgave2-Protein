using System.Web.Mvc;

namespace ITTWEB_Opgave2_Protein.Controllers
{
    /// <summary>
    /// Create an ActionResult and PartialView for each angular partial view you want to attatch to a route in the angular app.js file.
    /// </summary>
    public class AppController : Controller
    {
        public ActionResult Register()
        {
            return PartialView();
        }

        public ActionResult SignIn()
        {
            return PartialView();
        }

        public ActionResult Home()
        {
            return PartialView();
        }

        [Authorize]
        public ActionResult NewProtein()
        {
            return PartialView();
        }

        [Authorize]
        public ActionResult StatisticProtein()
        {
            return PartialView();
        }

        [Authorize]
        public ActionResult SetupProtein()
        {
            return PartialView();
        }
    }
}