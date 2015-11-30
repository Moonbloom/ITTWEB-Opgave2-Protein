using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ITTWEB_Opgave2_Protein.Controllers.Utility;
using ITTWEB_Opgave2_Protein.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;

namespace ITTWEB_Opgave2_Protein.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; }

        public AccountController()
        {

        }

        public AccountController(ApplicationUserManager userManager, ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<HttpResponseMessage> Register(RegisterBindingModel model)
        {
            var errors = ModelErrorChecker.Check(ModelState);

            if (errors.Count == 0)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    UserTypeId = model.UserTypeId,
                    Weight = model.Weight
                };

                if (UserManager.FindByEmail(model.Email) == null)
                {
                    var result = await UserManager.CreateAsync(user, model.Password);

                    if (!result.Succeeded)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, result.Errors);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Email address is already in use.");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, errors);
            }
            
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UserManager.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        #endregion
    }
}