using System;
using System.Data.Entity;

namespace ITTWEB_Opgave2_Protein.Models
{
    public class DbInitializer : DropCreateDatabaseAlways<DBContext>
    {
        protected override void Seed(DBContext context)
        {
            #region UserTypes
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
            #endregion

            #region FoodPosibility
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
            var kasper = new User
            {
                Id = "24bbcc10-5ab3-4beb-9261-da210cda00eb",
                UserTypeId = 1,
                Weight = 80,
                Email = "kasper",
                PasswordHash = "APKerMrbme1u5K+2OR8LRgGVcWBZltgTNXmK/JJgeulosJgMLJT3gfKVyE2WcgpYJg==",
                SecurityStamp = "4e9b0901-bf41-4d55-9e0c-4767454d8898",
                UserName = "kasper"
            };
            potato.Users.Add(kasper);
            greenBeans.Users.Add(kasper);
            ryeBread.Users.Add(kasper);

            context.Users.Add(kasper);

            var christian = new User
            {
                Id = "bd955647-3d5d-4bea-abcc-c47954ef0c56",
                UserTypeId = 1,
                Weight = 60,
                Email = "christian",
                PasswordHash = "AJPwSFNw21p7xAiX1PSDzpxMF1ukWrDWseoKC+n9grmKpbqHL/nhqq4SylJrSXksVQ==",
                SecurityStamp = "0172c2f0-c6d5-43bf-bb69-531abeb59a18",
                UserName = "christian"
            };
            potato.Users.Add(christian);
            ryeBread.Users.Add(christian);
            pork.Users.Add(christian);

            context.Users.Add(christian);
            #endregion

            #region FoodIntake
            var foodIntake1 = new FoodIntake
            {
                UserId = kasper.Id,
                Amount = 70,
                Date = DateTime.Now,
                FoodPosibilityId = potato.Id
            };
            context.FoodIntakes.Add(foodIntake1);

            var foodIntake2 = new FoodIntake
            {
                UserId = kasper.Id,
                Amount = 110,
                Date = DateTime.Now,
                FoodPosibilityId = greenBeans.Id
            };
            context.FoodIntakes.Add(foodIntake2);

            var foodIntake3 = new FoodIntake
            {
                UserId = kasper.Id,
                Amount = 90,
                Date = DateTime.Now,
                FoodPosibilityId = ryeBread.Id
            };
            context.FoodIntakes.Add(foodIntake3);
            #endregion

            context.SaveChanges();
        }
    }
}