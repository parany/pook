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
            modelBuilder.Entity<Book>()
                .HasMany(c => c.Categories).WithMany(i => i.Books)
                .Map(t => t.MapLeftKey("BookId")
                .MapRightKey("CategoryId")
                .ToTable("BookCategory"));
            modelBuilder.Entity<Book>()
                .HasMany(c => c.Authors).WithMany(i => i.Books)
                .Map(t => t.MapLeftKey("BookId")
                .MapRightKey("CategoryId")
                .ToTable("BookAuthor"));
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Firm> Firms { get; set; } 

        public DbSet<Category> Categories { get; set; } 

        public DbSet<Author> Authors { get; set; }
        
        public DbSet<AuthorRole> AuthorRoles { get; set; } 

        public DbSet<Editor> Editors { get; set; }

        public DbSet<Progression> Progressions { get; set; }

        public DbSet<Status> Statuses { get; set; }
    }
}
