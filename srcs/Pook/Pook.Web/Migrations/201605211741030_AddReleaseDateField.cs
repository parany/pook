namespace Pook.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReleaseDateField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "ReleaseDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "ReleaseDate");
        }
    }
}
