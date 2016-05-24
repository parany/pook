namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeFirmNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "FirmId", "dbo.Firms");
            DropIndex("dbo.Books", new[] { "FirmId" });
            AlterColumn("dbo.Books", "FirmId", c => c.Guid());
            CreateIndex("dbo.Books", "FirmId");
            AddForeignKey("dbo.Books", "FirmId", "dbo.Firms", "FirmId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "FirmId", "dbo.Firms");
            DropIndex("dbo.Books", new[] { "FirmId" });
            AlterColumn("dbo.Books", "FirmId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Books", "FirmId");
            AddForeignKey("dbo.Books", "FirmId", "dbo.Firms", "FirmId", cascadeDelete: true);
        }
    }
}
