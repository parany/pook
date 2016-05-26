namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookCategoryRemoveContent : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BookCategories", "CreatedOn");
            DropColumn("dbo.BookCategories", "UpdatedOn");
            DropColumn("dbo.BookCategories", "CreatedBy");
            DropColumn("dbo.BookCategories", "UpdatedBy");
            DropColumn("dbo.BookCategories", "SeoTitle");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookCategories", "SeoTitle", c => c.String());
            AddColumn("dbo.BookCategories", "UpdatedBy", c => c.Guid());
            AddColumn("dbo.BookCategories", "CreatedBy", c => c.Guid());
            AddColumn("dbo.BookCategories", "UpdatedOn", c => c.DateTime());
            AddColumn("dbo.BookCategories", "CreatedOn", c => c.DateTime());
        }
    }
}
