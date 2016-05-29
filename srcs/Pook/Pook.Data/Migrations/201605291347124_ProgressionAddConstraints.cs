namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProgressionAddConstraints : DbMigration
    {
        public override void Up()
        {
            DropIndex("User.Progression", new[] { "StatusId" });
            DropIndex("User.Progression", new[] { "BookId" });
            DropIndex("User.Progression", new[] { "UserId" });
            CreateIndex("User.Progression", new[] { "BookId", "UserId", "StatusId" }, unique: true, name: "IX_Progression");
        }
        
        public override void Down()
        {
            DropIndex("User.Progression", "IX_Progression");
            CreateIndex("User.Progression", "UserId");
            CreateIndex("User.Progression", "BookId");
            CreateIndex("User.Progression", "StatusId");
        }
    }
}
