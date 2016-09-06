namespace Pook.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Pook.Data.PookDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Pook.Data.PookDbContext context)
        {
            //var book1 = new Book
            //{
            //    BookId = Guid.NewGuid(),
            //    Title = "title1",
            //    Description = "description1",
            //    ReleaseDate = DateTime.Now,
            //};
            //var book2 = new Book
            //{
            //    BookId = Guid.NewGuid(),
            //    Title = "title2",
            //    Description = "description2",
            //    ReleaseDate = DateTime.Now,
            //};

            //var category1 = new Category
            //{
            //    CategoryId = Guid.NewGuid(),
            //    Title = "title1",
            //    Description = "description1"
            //};
            //var category2 = new Category
            //{
            //    CategoryId = Guid.NewGuid(),
            //    Title = "title2",
            //    Description = "description2"
            //};

            //book1.Categories.Add(category1);
            //book2.Categories.Add(category1);
            //book2.Categories.Add(category2);

            //context.Books.Add(book1);
            //context.Books.Add(book2);

            //context.SaveChanges();
            //base.Seed(context);
        }
    }
}
