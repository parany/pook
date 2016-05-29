using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<User>().ToTable("User", "User");
            modelBuilder.Entity<IdentityRole>().ToTable("Role", "User");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole", "User");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim", "User");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin", "User");
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Firm> Firms { get; set; } 

        public DbSet<Category> Categories { get; set; } 

        public DbSet<Author> Authors { get; set; }
        
        public DbSet<ResponsabilityType> ResponsabilityTypes { get; set; } 

        public DbSet<Responsability> Responsabilities { get; set; } 

        public DbSet<Editor> Editors { get; set; }

        public DbSet<Progression> Progressions { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Note> Notes { get; set; } 
    }
}
