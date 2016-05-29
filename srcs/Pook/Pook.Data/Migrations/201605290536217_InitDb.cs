namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Book.AuthorRole",
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
                "Book.Author",
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
                .ForeignKey("Book.AuthorRole", t => t.AuthorRoleId, cascadeDelete: true)
                .Index(t => t.AuthorRoleId);
            
            CreateTable(
                "Book.Book",
                c => new
                    {
                        BookId = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        FirmId = c.Guid(),
                        EditorId = c.Guid(),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedBy = c.Guid(),
                        SeoTitle = c.String(),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("Editor.Editor", t => t.EditorId)
                .ForeignKey("Editor.Firm", t => t.FirmId)
                .Index(t => t.FirmId)
                .Index(t => t.EditorId);
            
            CreateTable(
                "Book.Category",
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
            
            CreateTable(
                "Editor.Editor",
                c => new
                    {
                        EditorId = c.Guid(nullable: false),
                        Description = c.String(),
                        Address = c.String(),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedBy = c.Guid(),
                        SeoTitle = c.String(),
                    })
                .PrimaryKey(t => t.EditorId);
            
            CreateTable(
                "Editor.Firm",
                c => new
                    {
                        FirmId = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedBy = c.Guid(),
                        SeoTitle = c.String(),
                    })
                .PrimaryKey(t => t.FirmId);
            
            CreateTable(
                "User.Note",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        BookId = c.Guid(nullable: false),
                        Page = c.Int(nullable: false),
                        Description = c.String(),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedBy = c.Guid(),
                        SeoTitle = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.BookId })
                .ForeignKey("Book.Book", t => t.BookId, cascadeDelete: true)
                .ForeignKey("User.User", t => t.User_Id)
                .Index(t => t.BookId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "User.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "User.UserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("User.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "User.UserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("User.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "User.Progression",
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
                .ForeignKey("User.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("User.User", t => t.User_Id)
                .Index(t => t.StatusId)
                .Index(t => t.BookId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "User.Status",
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
            
            CreateTable(
                "User.UserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("User.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("User.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "User.Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.BookAuthor",
                c => new
                    {
                        BookId = c.Guid(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookId, t.CategoryId })
                .ForeignKey("Book.Book", t => t.BookId, cascadeDelete: true)
                .ForeignKey("Book.Author", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.BookCategory",
                c => new
                    {
                        BookId = c.Guid(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookId, t.CategoryId })
                .ForeignKey("Book.Book", t => t.BookId, cascadeDelete: true)
                .ForeignKey("Book.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("User.UserRole", "RoleId", "User.Role");
            DropForeignKey("User.UserRole", "UserId", "User.User");
            DropForeignKey("User.Progression", "User_Id", "User.User");
            DropForeignKey("User.Progression", "StatusId", "User.Status");
            DropForeignKey("User.Progression", "BookId", "Book.Book");
            DropForeignKey("User.Note", "User_Id", "User.User");
            DropForeignKey("User.UserLogin", "UserId", "User.User");
            DropForeignKey("User.UserClaim", "UserId", "User.User");
            DropForeignKey("User.Note", "BookId", "Book.Book");
            DropForeignKey("Book.Book", "FirmId", "Editor.Firm");
            DropForeignKey("Book.Book", "EditorId", "Editor.Editor");
            DropForeignKey("dbo.BookCategory", "CategoryId", "Book.Category");
            DropForeignKey("dbo.BookCategory", "BookId", "Book.Book");
            DropForeignKey("dbo.BookAuthor", "CategoryId", "Book.Author");
            DropForeignKey("dbo.BookAuthor", "BookId", "Book.Book");
            DropForeignKey("Book.Author", "AuthorRoleId", "Book.AuthorRole");
            DropIndex("dbo.BookCategory", new[] { "CategoryId" });
            DropIndex("dbo.BookCategory", new[] { "BookId" });
            DropIndex("dbo.BookAuthor", new[] { "CategoryId" });
            DropIndex("dbo.BookAuthor", new[] { "BookId" });
            DropIndex("User.Role", "RoleNameIndex");
            DropIndex("User.UserRole", new[] { "RoleId" });
            DropIndex("User.UserRole", new[] { "UserId" });
            DropIndex("User.Progression", new[] { "User_Id" });
            DropIndex("User.Progression", new[] { "BookId" });
            DropIndex("User.Progression", new[] { "StatusId" });
            DropIndex("User.UserLogin", new[] { "UserId" });
            DropIndex("User.UserClaim", new[] { "UserId" });
            DropIndex("User.User", "UserNameIndex");
            DropIndex("User.Note", new[] { "User_Id" });
            DropIndex("User.Note", new[] { "BookId" });
            DropIndex("Book.Book", new[] { "EditorId" });
            DropIndex("Book.Book", new[] { "FirmId" });
            DropIndex("Book.Author", new[] { "AuthorRoleId" });
            DropTable("dbo.BookCategory");
            DropTable("dbo.BookAuthor");
            DropTable("User.Role");
            DropTable("User.UserRole");
            DropTable("User.Status");
            DropTable("User.Progression");
            DropTable("User.UserLogin");
            DropTable("User.UserClaim");
            DropTable("User.User");
            DropTable("User.Note");
            DropTable("Editor.Firm");
            DropTable("Editor.Editor");
            DropTable("Book.Category");
            DropTable("Book.Book");
            DropTable("Book.Author");
            DropTable("Book.AuthorRole");
        }
    }
}
