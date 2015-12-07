using System;
using ITTWEB_Opgave2_Protein.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ITTWEB_Opgave2_Protein.Controllers
{
    public class WsStatisticController : ApiController
    {
        private readonly DBContext _db = new DBContext();

        [HttpGet]
        [Authorize]
        public List GetStatistic(DateTime Time)
        {
            var userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            var user = _db.Users.Include(a => a.FoodIntakes).Include(b => b.FoodPosibilities).FirstOrDefault(x => x.Id == userId);

             user.FoodIntakes.Where(x => x.Date.ToString("dd-MM-yyyy").Equals(Time.ToString("dd-MM-yyyy"))).ToList();
        }
    }
}