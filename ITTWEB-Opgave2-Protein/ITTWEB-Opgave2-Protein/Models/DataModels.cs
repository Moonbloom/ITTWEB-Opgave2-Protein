using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ITTWEB_Opgave2_Protein.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            FoodIntakes = new HashSet<FoodIntake>();
            FoodPosibilities = new HashSet<FoodPosibility>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public string Name { get; set; }
        public double Weight { get; set; }

        public int UserTypeId { get; set; }
        public virtual UserType UserType { get; set; }

        public virtual ICollection<FoodIntake> FoodIntakes { get; set; }
        public virtual ICollection<FoodPosibility> FoodPosibilities { get; set; } 
    }

    public class UserType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class FoodIntake
    {
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
        public FoodPosibility()
        {
            Users = new HashSet<User>();
            FoodIntakes = new HashSet<FoodIntake>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double ProteinRatio { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<FoodIntake> FoodIntakes { get; set; } 
    }
}