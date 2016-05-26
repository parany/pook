namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookCategoryAddVirtualMembers : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.BookCategories", "BookId");
            CreateIndex("dbo.BookCategories", "CategoryId");
            AddForeignKey("dbo.BookCategories", "BookId", "dbo.Books", "BookId", cascadeDelete: true);
            AddForeignKey("dbo.BookCategories", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.BookCategories", "BookId", "dbo.Books");
            DropIndex("dbo.BookCategories", new[] { "CategoryId" });
            DropIndex("dbo.BookCategories", new[] { "BookId" });
        }
    }
}
