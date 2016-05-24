namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeContentFieldNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Books", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.Books", "CreatedBy", c => c.Guid());
            AlterColumn("dbo.Books", "UpdatedBy", c => c.Guid());
            AlterColumn("dbo.Firms", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Firms", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.Firms", "CreatedBy", c => c.Guid());
            AlterColumn("dbo.Firms", "UpdatedBy", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Firms", "UpdatedBy", c => c.Guid(nullable: false));
            AlterColumn("dbo.Firms", "CreatedBy", c => c.Guid(nullable: false));
            AlterColumn("dbo.Firms", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Firms", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Books", "UpdatedBy", c => c.Guid(nullable: false));
            AlterColumn("dbo.Books", "CreatedBy", c => c.Guid(nullable: false));
            AlterColumn("dbo.Books", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Books", "CreatedOn", c => c.DateTime(nullable: false));
        }
    }
}
