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
            context.Users.Add(user);

            var foodPosibility1 = new FoodPosibility
            {
                Name = "Potato",
                ProteinRatio = 40
            };
            context.FoodPosibilities.Add(foodPosibility1);

            var foodIntake1 = new FoodIntake
            {
                UserId = user.Id,
                Amount = 70,
                Date = DateTime.Now,
                FoodPosibilityId = foodPosibility1.Id
            };
            context.FoodIntakes.Add(foodIntake1);

            context.SaveChanges();
        }
    }
}