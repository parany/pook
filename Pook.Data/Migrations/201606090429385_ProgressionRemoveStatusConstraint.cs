namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProgressionRemoveStatusConstraint : DbMigration
    {
        public override void Up()
        {
            DropIndex("User.Progression", "IX_Progression");
            CreateIndex("User.Progression", "StatusId");
            CreateIndex("User.Progression", new[] { "BookId", "UserId" }, name: "IX_Progression");
        }
        
        public override void Down()
        {
            DropIndex("User.Progression", "IX_Progression");
            DropIndex("User.Progression", new[] { "StatusId" });
            CreateIndex("User.Progression", new[] { "BookId", "UserId", "StatusId" }, unique: true, name: "IX_Progression");
        }
    }
}
