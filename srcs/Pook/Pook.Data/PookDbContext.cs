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

        static PookDbContext()
        {
            Database.SetInitializer<PookDbContext>(new PookContextDbInitializer());
        }

        public static PookDbContext Create()
        {
            return new PookDbContext();
        }

        public DbSet<Book> Books { get; set; }
    }
}
