using System.Web.Mvc;

namespace ITTWEB_Opgave2_Protein.Controllers
{
    public class MainController : Controller
    {
        /// <summary>
        /// This maps to the Main/Index.cshtml file. This file is the main view for the application.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}