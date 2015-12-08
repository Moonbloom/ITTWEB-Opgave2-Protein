using System;
using ITTWEB_Opgave2_Protein.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Objects;
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
        public List<IGrouping<DateTime, FoodIntake>> GetStatistic(DateTime Time)
        {
            var startDate = Time;
            var endDate = Time.AddDays(7);
            var userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            var foodIntake = _db.FoodIntakes.Where(food => food.UserId == userId
                                                           && food.Date >= startDate
                                                           && food.Date <= endDate).AsEnumerable().GroupBy(food =>food.Date.Date);
            var returnData = foodIntake.ToList();

            return returnData;
        }
    }
}