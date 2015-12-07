using System;
using ITTWEB_Opgave2_Protein.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;

namespace ITTWEB_Opgave2_Protein.Controllers
{
    public class WsProteinController : ApiController
    {
        private readonly DBContext _db = new DBContext();

        [HttpGet]
        //NO AUTH
        public IList<UserType> GetUserTypes()
        {
            return _db.UserTypes.ToList();
        }

        [HttpGet]
        [Authorize]
        public IList<FoodIntake> GetFoodIntakes()
        {
            var userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            var user = _db.Users.Include(a => a.FoodIntakes).Include(b => b.FoodPosibilities).FirstOrDefault(x => x.Id == userId);

            return user.FoodIntakes.Where(x => x.Date.ToString("dd-MM-yyyy").Equals(DateTime.Now.ToString("dd-MM-yyyy"))).ToList();
        }

        [HttpGet]
        [Authorize]
        public IList<FoodPosibility> GetFoodPosibilities()
        {
            var userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            var user = _db.Users.Include(b => b.FoodPosibilities).FirstOrDefault(x => x.Id == userId);

            return user.FoodPosibilities.ToList();
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage PostFoodIntake(FoodIntake item)
        {
            var errors = CheckErrors(ModelState.Values.ToList());

            if (errors.Count == 0)
            {
                try
                {
                    item.Date = DateTime.Now;
                    item.UserId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
                    item.FoodPosibility = null;

                    _db.FoodIntakes.Add(item);
                    _db.SaveChanges();

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

        [HttpPost]
        [Authorize]
        public HttpResponseMessage UpdateFoodIntake(FoodIntake item)
        {
            var errors = CheckErrors(ModelState.Values.ToList());

            if (errors.Count == 0)
            {
                try
                {
                    _db.FoodIntakes.AddOrUpdate(item);
                    _db.SaveChanges();

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

        [HttpPost]
        [Authorize]
        public HttpResponseMessage DeleteFoodIntake([FromBody] int foodIntakeId)
        {
            var errors = CheckErrors(ModelState.Values.ToList());

            if (errors.Count == 0)
            {
                try
                {
                    var foodIntakeitem = _db.FoodIntakes.FirstOrDefault(x => x.Id == foodIntakeId);
                    _db.FoodIntakes.Remove(foodIntakeitem);
                    _db.SaveChanges();

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

        #region Private functions
        private List<string> CheckErrors(IEnumerable<ModelState> list)
        {
            var errors = new List<string>();

            foreach (var s in list)
            {
                foreach (var e in s.Errors)
                {
                    if (e.ErrorMessage != null && e.ErrorMessage.Trim() != "")
                    {
                        errors.Add(e.ErrorMessage);
                    }
                }
            }

            return errors;
        }
        #endregion
    }
}