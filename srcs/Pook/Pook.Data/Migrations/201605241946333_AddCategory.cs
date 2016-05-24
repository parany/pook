namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookCategories",
                c => new
                    {
                        BookCategoryId = c.Guid(nullable: false),
                        BookId = c.Guid(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedBy = c.Guid(),
                        SeoTitle = c.String(),
                        BookCategory_BookCategoryId = c.Guid(),
                    })
                .PrimaryKey(t => t.BookCategoryId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.BookCategories", t => t.BookCategory_BookCategoryId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.CategoryId)
                .Index(t => t.BookCategory_BookCategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedBy = c.Guid(),
                        SeoTitle = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.BookCategories", "BookCategory_BookCategoryId", "dbo.BookCategories");
            DropForeignKey("dbo.BookCategories", "BookId", "dbo.Books");
            DropIndex("dbo.BookCategories", new[] { "BookCategory_BookCategoryId" });
            DropIndex("dbo.BookCategories", new[] { "CategoryId" });
            DropIndex("dbo.BookCategories", new[] { "BookId" });
            DropTable("dbo.Categories");
            DropTable("dbo.BookCategories");
        }
    }
}
