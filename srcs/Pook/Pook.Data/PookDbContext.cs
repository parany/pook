using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Pook.Data.Entities;

namespace Pook.Data
{
    public class PookDbContext : IdentityDbContext<User>
    {
        public PookDbContext()
            : base("name=PookDb")
        {
        }

        static PookDbContext()
        {
            Database.SetInitializer(new PookDbInitializer());
        }

        public static PookDbContext Create()
        {
            return new PookDbContext();
        }

        public DbSet<Book> Books { get; set; } 
    }
}
