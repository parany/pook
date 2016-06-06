namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProgressionEditPageNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("User.Progression", "Page", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("User.Progression", "Page", c => c.Int(nullable: false));
        }
    }
}
