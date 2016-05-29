namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProgressionRemoveBookIndexRemoveUserIndexAddProgressionId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("User.Progression");
            AddColumn("User.Progression", "ProgressionId", c => c.Guid(nullable: false));
            AddPrimaryKey("User.Progression", "ProgressionId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("User.Progression");
            DropColumn("User.Progression", "ProgressionId");
            AddPrimaryKey("User.Progression", new[] { "StatusId", "BookId", "UserId" });
        }
    }
}
