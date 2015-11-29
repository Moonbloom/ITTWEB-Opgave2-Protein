using ITTWEB_Opgave2_Protein.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ITTWEB_Opgave2_Protein.Controllers
{
    public class WsProteinController : ApiController
    {
        private readonly DBContext _db = new DBContext();

        public RoleManager<IdentityRole> RoleManager { get; private set; }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        [Authorize]
        public ICollection<FoodIntake> GetFoodIntakes()
        {
            var userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();

            var currentUser = UserManager.FindById(userId);
            return currentUser.FoodIntakes;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage PostFoodIntake(FoodIntake item)
        {
            var modelStateErrors = ModelState.Values.ToList();

            var errors = new List<string>();

            foreach (var s in modelStateErrors)
            {
                foreach (var e in s.Errors)
                {
                    if (e.ErrorMessage != null && e.ErrorMessage.Trim() != "")
                    {
                        errors.Add(e.ErrorMessage);
                    }
                }
            }

            if (errors.Count == 0)
            {
                try
                {
                    var userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();

                    var currentUser = UserManager.FindById(userId);
                    currentUser.FoodIntakes.Add(item);

                    UserManager.Update(currentUser);
                    return Request.CreateResponse(HttpStatusCode.Accepted);
                }
                catch
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
            else
            {
                return Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, errors);
            }
        }
    }
}