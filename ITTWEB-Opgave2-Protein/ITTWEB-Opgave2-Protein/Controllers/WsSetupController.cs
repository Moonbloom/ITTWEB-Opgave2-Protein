using ITTWEB_Opgave2_Protein.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace ITTWEB_Opgave2_Protein.Controllers
{
    public class WsSetupController : ApiController
    {
        private readonly DBContext _db = new DBContext();

        [HttpGet]
        [Authorize]
        public IList<FoodPosibility> GetAllFoodPosibilities()
        {
            return _db.FoodPosibilities.ToList();
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
        public void PostFoodPosibilities(IList<FoodPosibility> userFoodPosibilities)
        {
            var userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            var user = _db.Users.Include(b => b.FoodPosibilities).FirstOrDefault(x => x.Id == userId);

            var allFoodPos = _db.FoodPosibilities.ToList();
            var actualUserFoodPos = new List<FoodPosibility>();
            foreach (var userFoodPosibility in userFoodPosibilities)
            {
                actualUserFoodPos.Add(_db.FoodPosibilities.Find(userFoodPosibility.Id));
            }

            foreach (var foodPos in allFoodPos)
            {
                if (!foodPos.Users.Contains(user) && actualUserFoodPos.Contains(foodPos))
                {
                    foodPos.Users.Add(user);
                }
                else if (foodPos.Users.Contains(user) && !actualUserFoodPos.Contains(foodPos))
                {
                    foodPos.Users.Remove(user);
                }
            }
            _db.SaveChanges();
        }
    }
}