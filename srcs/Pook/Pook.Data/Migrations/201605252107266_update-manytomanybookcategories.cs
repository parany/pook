namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemanytomanybookcategories : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookCategories", "BookId", "dbo.Books");
            DropForeignKey("dbo.BookCategories", "BookCategory_BookCategoryId", "dbo.BookCategories");
            DropForeignKey("dbo.BookCategories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.BookCategories", new[] { "BookId" });
            DropIndex("dbo.BookCategories", new[] { "CategoryId" });
            DropIndex("dbo.BookCategories", new[] { "BookCategory_BookCategoryId" });
            CreateTable(
                "dbo.CategoryBooks",
                c => new
                    {
                        Category_CategoryId = c.Guid(nullable: false),
                        Book_BookId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_CategoryId, t.Book_BookId })
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_BookId, cascadeDelete: true)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.Book_BookId);
            
            DropTable("dbo.BookCategories");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.BookCategoryId);
            
            DropForeignKey("dbo.CategoryBooks", "Book_BookId", "dbo.Books");
            DropForeignKey("dbo.CategoryBooks", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.CategoryBooks", new[] { "Book_BookId" });
            DropIndex("dbo.CategoryBooks", new[] { "Category_CategoryId" });
            DropTable("dbo.CategoryBooks");
            CreateIndex("dbo.BookCategories", "BookCategory_BookCategoryId");
            CreateIndex("dbo.BookCategories", "CategoryId");
            CreateIndex("dbo.BookCategories", "BookId");
            AddForeignKey("dbo.BookCategories", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
            AddForeignKey("dbo.BookCategories", "BookCategory_BookCategoryId", "dbo.BookCategories", "BookCategoryId");
            AddForeignKey("dbo.BookCategories", "BookId", "dbo.Books", "BookId", cascadeDelete: true);
        }
    }
}
