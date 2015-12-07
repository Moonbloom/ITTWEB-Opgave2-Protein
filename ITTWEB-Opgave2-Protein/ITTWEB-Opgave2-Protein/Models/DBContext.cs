using System.Data.Entity;
using System.Web.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace ITTWEB_Opgave2_Protein.Models
{
    public class DBContext : IdentityDbContext<User>
    {
        public DBContext() : base("applicationDB")
        {
            Database.SetInitializer(new DbInitializer());
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }

        public static DBContext Create()
        {
            return new DBContext();
        }

        public DbSet<FoodIntake> FoodIntakes { get; set; }
        public DbSet<FoodPosibility> FoodPosibilities { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
    }
}