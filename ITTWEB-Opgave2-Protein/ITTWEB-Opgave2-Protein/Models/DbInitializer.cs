using System;
using System.Data.Entity;

namespace ITTWEB_Opgave2_Protein.Models
{
    public class DbInitializer : DropCreateDatabaseAlways<DBContext>
    {
        protected override void Seed(DBContext context)
        {
            var userType1 = new UserType
            {
                Id = 1,
                Name = "Adult"
            };
            context.UserTypes.Add(userType1);

            var userType2 = new UserType
            {
                Id = 2,
                Name = "Sick/Weak"
            };
            context.UserTypes.Add(userType2);

            var userType3 = new UserType
            {
                Id = 3,
                Name = "Training Adult"
            };
            context.UserTypes.Add(userType3);

            #region FoodPosibilitySeed
            var potato = new FoodPosibility
            {
                Id = 1,
                Name = "Potato",
                ProteinRatio = 0.019,
            };
            context.FoodPosibilities.Add(potato);

            var greenBeans = new FoodPosibility
            {
                Id = 2,
                Name = "Green Beans",
                ProteinRatio = 0.02
            };
            context.FoodPosibilities.Add(greenBeans);

            var ryeBread = new FoodPosibility
            {
                Id = 3,
                Name = "Rye bread",
                ProteinRatio = 0.062
            };
            context.FoodPosibilities.Add(ryeBread);

            var oatmeal = new FoodPosibility
            {
                Id = 4,
                Name = "Oatmeal",
                ProteinRatio = 0.133
            };
            context.FoodPosibilities.Add(oatmeal);

            var milk = new FoodPosibility
            {
                Id = 5,
                Name = "Milk",
                ProteinRatio = 0.133
            };
            context.FoodPosibilities.Add(milk);

            var egg = new FoodPosibility
            {
                Id = 6,
                Name = "Egg",
                ProteinRatio = 0.126
            };
            context.FoodPosibilities.Add(egg);

            var chicken = new FoodPosibility
            {
                Id = 7,
                Name = "Chicken",
                ProteinRatio = 0.20
            };
            context.FoodPosibilities.Add(chicken);

            var pork = new FoodPosibility
            {
                Id = 8,
                Name = "Pork",
                ProteinRatio = 0.20
            };
            context.FoodPosibilities.Add(pork);

            var beef = new FoodPosibility()
            {
                Id = 9,
                Name = "Beef",
                ProteinRatio = 0.20
            };
            context.FoodPosibilities.Add(beef);
            #endregion

            #region User
            var user = new User
            {
                Id = "24bbcc10-5ab3-4beb-9261-da210cda00eb",
                UserTypeId = 1,
                Weight = 80,
                Email = "lol",
                PasswordHash = "ANyTo3lyBaGCuF7QqPr1pCZel2nRM2rWYjqepLSqtNoYfoPTO9X7bZICaoJL7jSISg==",
                SecurityStamp = "704ea163-b098-4dea-b027-bbd1bea72a0d",
                UserName = "lol"
            };
            user.FoodPosibilities.Add(potato);
            user.FoodPosibilities.Add(greenBeans);
            user.FoodPosibilities.Add(ryeBread);
            context.Users.Add(user);
            #endregion

            #region Food intake
            var foodIntake1 = new FoodIntake
            {
                UserId = user.Id,
                Amount = 70,
                Date = DateTime.Now.AddDays(-1),
                FoodPosibilityId = potato.Id
            };
            context.FoodIntakes.Add(foodIntake1);

            var foodIntake2 = new FoodIntake
            {
                UserId = user.Id,
                Amount = 110,
                Date = DateTime.Now,
                FoodPosibilityId = greenBeans.Id
            };
            context.FoodIntakes.Add(foodIntake2);

            var foodIntake3 = new FoodIntake
            {
                UserId = user.Id,
                Amount = 90,
                Date = DateTime.Now.AddHours(1),
                FoodPosibilityId = ryeBread.Id
            };
            context.FoodIntakes.Add(foodIntake3);
            #endregion

            context.SaveChanges();
        }
    }
}