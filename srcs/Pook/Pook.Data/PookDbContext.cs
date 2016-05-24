using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Pook.Data.Entities;

namespace Pook.Data
{
    public class PookDbContext : IdentityDbContext<User>
    {
        public PookDbContext()
            : base("PookDbContext", throwIfV1Schema: false)
        {
        }

        public static PookDbContext Create()
        {
            return new PookDbContext();
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Firm> Firms { get; set; } 

        public DbSet<Category> Categories { get; set; } 
    }
}
