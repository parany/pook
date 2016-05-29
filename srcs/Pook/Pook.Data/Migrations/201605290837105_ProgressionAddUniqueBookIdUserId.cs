namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProgressionAddUniqueBookIdUserId : DbMigration
    {
        public override void Up()
        {
            DropIndex("User.Progression", new[] { "BookId" });
            CreateIndex("User.Progression", new[] { "BookId", "UserId" }, unique: true, name: "IX_Progression");
        }
        
        public override void Down()
        {
            DropIndex("User.Progression", "IX_Progression");
            CreateIndex("User.Progression", "BookId");
        }
    }
}
