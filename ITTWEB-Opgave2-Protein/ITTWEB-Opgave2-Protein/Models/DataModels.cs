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

        public string FirstName { get; set; }

        public virtual List<TodoItem> TodoItems { get; set; }
    }
    
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        public string Task { get; set; }
        public bool Completed { get; set; }
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

        public DbSet<TodoItem> Todos { get; set; }

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