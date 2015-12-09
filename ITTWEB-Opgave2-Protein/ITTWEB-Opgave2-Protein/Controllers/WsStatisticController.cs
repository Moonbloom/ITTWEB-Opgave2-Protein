using System;
using ITTWEB_Opgave2_Protein.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace ITTWEB_Opgave2_Protein.Controllers
{
    public class WsStatisticController : ApiController
    {
        private readonly DBContext _db = new DBContext();

        [HttpGet]
        [Authorize]
        public List<IGrouping<DateTime, FoodIntake>> GetStatistic(DateTime time)
        {
            var startDate = time;
            var endDate = time.AddDays(7);
            var userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            var foodIntake = _db.FoodIntakes.Where(food => food.UserId == userId
                                                           && food.Date >= startDate
                                                           && food.Date <= endDate).AsEnumerable().GroupBy(food =>food.Date.Date);
            var returnData = foodIntake.ToList();

            return returnData;
        }
    }
}