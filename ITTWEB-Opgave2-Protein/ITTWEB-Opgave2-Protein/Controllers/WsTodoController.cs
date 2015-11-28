using ITTWEB_Opgave2_Protein.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace ITTWEB_Opgave2_Protein.Controllers
{
    public class WsTodoController : ApiController
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
        public List<TodoItem> GetUserTodoItems()
        {
            var userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();

            var currentUser = UserManager.FindById(userId);
            return currentUser.TodoItems;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage PostTodoItem(TodoItemViewModel item)
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
                    currentUser.TodoItems.Add(new TodoItem()
                    {
                        Completed = false,
                        Task = item.task
                    });

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

            //var user = _db.Users.Where(u => u.FirstName == "Test").FirstOrDefault();
        }

        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> CompleteTodoItem(int id)
        {
            var item = _db.Todos.FirstOrDefault(t => t.Id == id);
            if (item != null)
            {
                item.Completed = true;
                await _db.SaveChangesAsync();
            }

            return Request.CreateResponse(HttpStatusCode.Accepted);
        }

        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> DeleteTodoItem(int id)
        {
            var item = _db.Todos.FirstOrDefault(t => t.Id == id);
            if (item != null)
            {
                _db.Todos.Remove(item);
                await _db.SaveChangesAsync();
            }

            return Request.CreateResponse(HttpStatusCode.Accepted);
        }
    }
}