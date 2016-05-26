namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookCategories",
                c => new
                    {
                        BookId = c.Guid(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                        Observation = c.String(),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedBy = c.Guid(),
                        SeoTitle = c.String(),
                    })
                .PrimaryKey(t => new { t.BookId, t.CategoryId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BookCategories");
        }
    }
}
