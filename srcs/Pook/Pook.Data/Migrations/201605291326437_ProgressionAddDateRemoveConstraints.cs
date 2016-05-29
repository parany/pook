namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProgressionAddDateRemoveConstraints : DbMigration
    {
        public override void Up()
        {
            DropIndex("User.Progression", "IX_Progression");
            AddColumn("User.Progression", "Date", c => c.DateTime(nullable: false));
            CreateIndex("User.Progression", "BookId");
            CreateIndex("User.Progression", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("User.Progression", new[] { "UserId" });
            DropIndex("User.Progression", new[] { "BookId" });
            DropColumn("User.Progression", "Date");
            CreateIndex("User.Progression", new[] { "BookId", "UserId" }, unique: true, name: "IX_Progression");
        }
    }
}
