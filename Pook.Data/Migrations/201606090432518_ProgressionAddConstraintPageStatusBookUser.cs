namespace Pook.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ProgressionAddConstraintPageStatusBookUser : DbMigration
    {
        public override void Up()
        {
            DropIndex("User.Progression", new[] { "StatusId" });
            DropIndex("User.Progression", "IX_Progression");
            CreateIndex("User.Progression", new[] { "BookId", "UserId", "StatusId", "Page" }, unique: true, name: "IX_Progression");
        }
        
        public override void Down()
        {
            DropIndex("User.Progression", "IX_Progression");
            CreateIndex("User.Progression", new[] { "BookId", "UserId" }, name: "IX_Progression");
            CreateIndex("User.Progression", "StatusId");
        }
    }
}
