using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ITTWEB_Opgave2_Protein.Models
{
    public class User : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public string Name { get; set; }
        public UserType UserType { get; set; }
        public double Weight { get; set; }

        public virtual List<TodoItem> TodoItems { get; set; }
        public virtual ICollection<FoodIntake> FoodIntakes { get; set; }
        public virtual ICollection<FoodPosibility> FoodPosibilities { get; set; } 
    }

    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        public string Task { get; set; }
        public bool Completed { get; set; }
    }

    public enum UserType
    {
        Adult = 0,
        TrainingAdult = 1,
        Sick = 2
    }

    public class FoodIntake
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }

        public int FoodPosibilityId { get; set; }
        public virtual FoodPosibility FoodPosibility { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }

    public class FoodPosibility
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProteinRatio { get; set; }

        public virtual ICollection<User> Users { get; set; } 
    }

    public class DBContext : IdentityDbContext<User>
    {
        public DBContext() : base("applicationDB")
        {
            Database.SetInitializer(new DBInitializer());
        }

        //Override default table names
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public static DBContext Create()
        {
            return new DBContext();
        }

        public DbSet<FoodIntake> FoodIntakes { get; set; }
        public DbSet<FoodPosibility> FoodPosibilities { get; set; }
        public DbSet<TodoItem> Todos { get; set; }
    }

    //This function will ensure the database is created and seeded with any default data.
    public class DBInitializer : DropCreateDatabaseAlways<DBContext>
    {
        protected override void Seed(DBContext context)
        {
            //Create an seed data you wish in the database.

            var user = new User
            {
                Id = "24bbcc10-5ab3-4beb-9261-da210cda00eb",
                UserType = 0,
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