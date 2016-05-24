namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFirm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Firms",
                c => new
                    {
                        FirmId = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        UpdatedBy = c.Guid(nullable: false),
                        SeoTitle = c.String(),
                    })
                .PrimaryKey(t => t.FirmId);
            
            AddColumn("dbo.Books", "FirmId", c => c.Guid(nullable: true));
            AddColumn("dbo.Books", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Books", "UpdatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Books", "CreatedBy", c => c.Guid(nullable: false));
            AddColumn("dbo.Books", "UpdatedBy", c => c.Guid(nullable: false));
            AddColumn("dbo.Books", "SeoTitle", c => c.String());
            CreateIndex("dbo.Books", "FirmId");
            AddForeignKey("dbo.Books", "FirmId", "dbo.Firms", "FirmId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "FirmId", "dbo.Firms");
            DropIndex("dbo.Books", new[] { "FirmId" });
            DropColumn("dbo.Books", "SeoTitle");
            DropColumn("dbo.Books", "UpdatedBy");
            DropColumn("dbo.Books", "CreatedBy");
            DropColumn("dbo.Books", "UpdatedOn");
            DropColumn("dbo.Books", "CreatedOn");
            DropColumn("dbo.Books", "FirmId");
            DropTable("dbo.Firms");
        }
    }
}
