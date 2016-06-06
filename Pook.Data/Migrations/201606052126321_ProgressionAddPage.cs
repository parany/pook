namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProgressionAddPage : DbMigration
    {
        public override void Up()
        {
            AddColumn("User.Progression", "Page", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("User.Progression", "Page");
        }
    }
}
