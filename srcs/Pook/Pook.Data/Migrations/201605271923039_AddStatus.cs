namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Book.Progression",
                c => new
                    {
                        StatusId = c.Guid(nullable: false),
                        BookId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedBy = c.Guid(),
                        SeoTitle = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.StatusId, t.BookId, t.UserId })
                .ForeignKey("Book.Book", t => t.BookId, cascadeDelete: true)
                .ForeignKey("Book.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.StatusId)
                .Index(t => t.BookId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "Book.Status",
                c => new
                    {
                        StatusId = c.Guid(nullable: false),
                        Title = c.String(),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedBy = c.Guid(),
                        SeoTitle = c.String(),
                    })
                .PrimaryKey(t => t.StatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Book.Progression", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("Book.Progression", "StatusId", "Book.Status");
            DropForeignKey("Book.Progression", "BookId", "Book.Book");
            DropIndex("Book.Progression", new[] { "User_Id" });
            DropIndex("Book.Progression", new[] { "BookId" });
            DropIndex("Book.Progression", new[] { "StatusId" });
            DropTable("Book.Status");
            DropTable("Book.Progression");
        }
    }
}
