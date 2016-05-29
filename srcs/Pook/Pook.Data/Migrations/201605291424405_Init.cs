namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Book.Author",
                c => new
                    {
                        AuthorId = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Description = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.AuthorId);
            
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
                        CategoryId = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedBy = c.Guid(),
                        SeoTitle = c.String(),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("Book.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("Editor.Editor", t => t.EditorId)
                .ForeignKey("Editor.Firm", t => t.FirmId)
                .Index(t => t.FirmId)
                .Index(t => t.EditorId)
                .Index(t => t.CategoryId);
            
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
                        Title = c.String(),
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
                        UserId = c.String(maxLength: 128),
                        BookId = c.Guid(nullable: false),
                        NoteId = c.Guid(nullable: false),
                        Page = c.Int(nullable: false),
                        Description = c.String(),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedBy = c.Guid(),
                        SeoTitle = c.String(),
                    })
                .PrimaryKey(t => t.NoteId)
                .ForeignKey("Book.Book", t => t.BookId, cascadeDelete: true)
                .ForeignKey("User.User", t => t.UserId)
                .Index(t => new { t.UserId, t.BookId, t.Page }, unique: true, name: "IX_Note");
            
            CreateTable(
                "User.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                        DateOfBirth = c.DateTime(),
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
                        ProgressionId = c.Guid(nullable: false),
                        StatusId = c.Guid(nullable: false),
                        BookId = c.Guid(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedBy = c.Guid(),
                        SeoTitle = c.String(),
                    })
                .PrimaryKey(t => t.ProgressionId)
                .ForeignKey("Book.Book", t => t.BookId, cascadeDelete: true)
                .ForeignKey("User.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("User.User", t => t.UserId)
                .Index(t => new { t.BookId, t.UserId, t.StatusId }, unique: true, name: "IX_Progression");
            
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
                "Book.Responsability",
                c => new
                    {
                        ResponsabilityId = c.Guid(nullable: false),
                        ResponsabilityTypeId = c.Guid(nullable: false),
                        AuthorId = c.Guid(nullable: false),
                        BookId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ResponsabilityId)
                .ForeignKey("Book.Author", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("Book.Book", t => t.BookId, cascadeDelete: true)
                .ForeignKey("Book.ResponsabilityType", t => t.ResponsabilityTypeId, cascadeDelete: true)
                .Index(t => t.ResponsabilityTypeId)
                .Index(t => t.AuthorId)
                .Index(t => t.BookId);
            
            CreateTable(
                "Book.ResponsabilityType",
                c => new
                    {
                        ResponsabilityTypeId = c.Guid(nullable: false),
                        Title = c.String(),
                        Desription = c.String(),
                    })
                .PrimaryKey(t => t.ResponsabilityTypeId);
            
            CreateTable(
                "User.Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("User.UserRole", "RoleId", "User.Role");
            DropForeignKey("Book.Responsability", "ResponsabilityTypeId", "Book.ResponsabilityType");
            DropForeignKey("Book.Responsability", "BookId", "Book.Book");
            DropForeignKey("Book.Responsability", "AuthorId", "Book.Author");
            DropForeignKey("User.UserRole", "UserId", "User.User");
            DropForeignKey("User.Progression", "UserId", "User.User");
            DropForeignKey("User.Progression", "StatusId", "User.Status");
            DropForeignKey("User.Progression", "BookId", "Book.Book");
            DropForeignKey("User.Note", "UserId", "User.User");
            DropForeignKey("User.UserLogin", "UserId", "User.User");
            DropForeignKey("User.UserClaim", "UserId", "User.User");
            DropForeignKey("User.Note", "BookId", "Book.Book");
            DropForeignKey("Book.Book", "FirmId", "Editor.Firm");
            DropForeignKey("Book.Book", "EditorId", "Editor.Editor");
            DropForeignKey("Book.Book", "CategoryId", "Book.Category");
            DropIndex("User.Role", "RoleNameIndex");
            DropIndex("Book.Responsability", new[] { "BookId" });
            DropIndex("Book.Responsability", new[] { "AuthorId" });
            DropIndex("Book.Responsability", new[] { "ResponsabilityTypeId" });
            DropIndex("User.UserRole", new[] { "RoleId" });
            DropIndex("User.UserRole", new[] { "UserId" });
            DropIndex("User.Progression", "IX_Progression");
            DropIndex("User.UserLogin", new[] { "UserId" });
            DropIndex("User.UserClaim", new[] { "UserId" });
            DropIndex("User.User", "UserNameIndex");
            DropIndex("User.Note", "IX_Note");
            DropIndex("Book.Book", new[] { "CategoryId" });
            DropIndex("Book.Book", new[] { "EditorId" });
            DropIndex("Book.Book", new[] { "FirmId" });
            DropTable("User.Role");
            DropTable("Book.ResponsabilityType");
            DropTable("Book.Responsability");
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
        }
    }
}
