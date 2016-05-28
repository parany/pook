namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
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
                .ForeignKey("Book.Editor", t => t.EditorId)
                .ForeignKey("Book.Firm", t => t.FirmId)
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
                "Book.Editor",
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
                "Book.Firm",
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
                "dbo.Note",
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
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.BookId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
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
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("Book.Progression", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("Book.Progression", "StatusId", "Book.Status");
            DropForeignKey("Book.Progression", "BookId", "Book.Book");
            DropForeignKey("dbo.Note", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Note", "BookId", "Book.Book");
            DropForeignKey("Book.Book", "FirmId", "Book.Firm");
            DropForeignKey("Book.Book", "EditorId", "Book.Editor");
            DropForeignKey("dbo.BookCategory", "CategoryId", "Book.Category");
            DropForeignKey("dbo.BookCategory", "BookId", "Book.Book");
            DropForeignKey("dbo.BookAuthor", "CategoryId", "Book.Author");
            DropForeignKey("dbo.BookAuthor", "BookId", "Book.Book");
            DropForeignKey("Book.Author", "AuthorRoleId", "Book.AuthorRole");
            DropIndex("dbo.BookCategory", new[] { "CategoryId" });
            DropIndex("dbo.BookCategory", new[] { "BookId" });
            DropIndex("dbo.BookAuthor", new[] { "CategoryId" });
            DropIndex("dbo.BookAuthor", new[] { "BookId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("Book.Progression", new[] { "User_Id" });
            DropIndex("Book.Progression", new[] { "BookId" });
            DropIndex("Book.Progression", new[] { "StatusId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Note", new[] { "User_Id" });
            DropIndex("dbo.Note", new[] { "BookId" });
            DropIndex("Book.Book", new[] { "EditorId" });
            DropIndex("Book.Book", new[] { "FirmId" });
            DropIndex("Book.Author", new[] { "AuthorRoleId" });
            DropTable("dbo.BookCategory");
            DropTable("dbo.BookAuthor");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("Book.Status");
            DropTable("Book.Progression");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Note");
            DropTable("Book.Firm");
            DropTable("Book.Editor");
            DropTable("Book.Category");
            DropTable("Book.Book");
            DropTable("Book.Author");
            DropTable("Book.AuthorRole");
        }
    }
}
