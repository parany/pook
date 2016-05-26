namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Editors",
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
            
            AddColumn("dbo.Books", "EditorId", c => c.Guid());
            CreateIndex("dbo.Books", "EditorId");
            AddForeignKey("dbo.Books", "EditorId", "dbo.Editors", "EditorId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "EditorId", "dbo.Editors");
            DropIndex("dbo.Books", new[] { "EditorId" });
            DropColumn("dbo.Books", "EditorId");
            DropTable("dbo.Editors");
        }
    }
}
