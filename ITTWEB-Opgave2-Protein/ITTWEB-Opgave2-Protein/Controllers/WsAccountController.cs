using ITTWEB_Opgave2_Protein.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ITTWEB_Opgave2_Protein.Controllers
{
    public class WsAccountController : ApiController
    {
        private DBContext _db = new DBContext();
        //HttpContext httpContext = new HttpContext(new Http

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

        //Based on the OWIN authentication, finds the current authenticated username.        
        public string GetCurrentUserName()
        {
            return Request.GetOwinContext().Authentication.User.Identity.Name;
        }

        //Based on the OWIN authentication, tells if the user is authenticated.
        //UNTESTED
        public bool GetIsLoggedIn()
        {
            return Request.GetOwinContext().Authentication.User.Identity.IsAuthenticated;
        }

        //Get the userID from the Request context, and pass the ID into the usermanager to get the current user roles.
        //UNTESTED
        public async Task<IEnumerable<string>> GetCurrentUserRoles()
        {
            return await UserManager.GetRolesAsync(Request.GetOwinContext().Authentication.User.Identity.GetUserId());
        }
    }
}