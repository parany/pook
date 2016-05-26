namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class author : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthorRoles",
                c => new
                    {
                        AuthorRoleId = c.Guid(nullable: false),
                        Title = c.String(),
                        Desription = c.String(),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedBy = c.Guid(),
                        SeoTitle = c.String(),
                    })
                .PrimaryKey(t => t.AuthorRoleId);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorId = c.Guid(nullable: false),
                        AuthorRoleId = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Description = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedBy = c.Guid(),
                        SeoTitle = c.String(),
                    })
                .PrimaryKey(t => t.AuthorId)
                .ForeignKey("dbo.AuthorRoles", t => t.AuthorRoleId, cascadeDelete: true)
                .Index(t => t.AuthorRoleId);
            
            CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        BookId = c.Guid(nullable: false),
                        AuthorId = c.Guid(nullable: false),
                        Observation = c.String(),
                    })
                .PrimaryKey(t => new { t.BookId, t.AuthorId })
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.BookAuthor1",
                c => new
                    {
                        Book_BookId = c.Guid(nullable: false),
                        Author_AuthorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_BookId, t.Author_AuthorId })
                .ForeignKey("dbo.Books", t => t.Book_BookId, cascadeDelete: true)
                .ForeignKey("dbo.Authors", t => t.Author_AuthorId, cascadeDelete: true)
                .Index(t => t.Book_BookId)
                .Index(t => t.Author_AuthorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookAuthors", "BookId", "dbo.Books");
            DropForeignKey("dbo.BookAuthors", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.BookAuthor1", "Author_AuthorId", "dbo.Authors");
            DropForeignKey("dbo.BookAuthor1", "Book_BookId", "dbo.Books");
            DropForeignKey("dbo.Authors", "AuthorRoleId", "dbo.AuthorRoles");
            DropIndex("dbo.BookAuthor1", new[] { "Author_AuthorId" });
            DropIndex("dbo.BookAuthor1", new[] { "Book_BookId" });
            DropIndex("dbo.BookAuthors", new[] { "AuthorId" });
            DropIndex("dbo.BookAuthors", new[] { "BookId" });
            DropIndex("dbo.Authors", new[] { "AuthorRoleId" });
            DropTable("dbo.BookAuthor1");
            DropTable("dbo.BookAuthors");
            DropTable("dbo.Authors");
            DropTable("dbo.AuthorRoles");
        }
    }
}
