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

        public virtual ICollection<FoodIntake> FoodIntakes { get; set; }
        public virtual ICollection<FoodPosibility> FoodPosibilities { get; set; } 
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

    }

    //This function will ensure the database is created and seeded with any default data.
    public class DBInitializer : CreateDatabaseIfNotExists<DBContext>
    {
        protected override void Seed(DBContext context)
        {
            //Create an seed data you wish in the database.
        }
    }
}