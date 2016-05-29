namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProgressionUpdateUserIdToString : DbMigration
    {
        public override void Up()
        {
            DropIndex("User.Progression", "IX_Progression");
            DropIndex("User.Progression", new[] { "User_Id" });
            DropColumn("User.Progression", "UserId");
            RenameColumn(table: "User.Progression", name: "User_Id", newName: "UserId");
            AlterColumn("User.Progression", "UserId", c => c.String(maxLength: 128));
            CreateIndex("User.Progression", new[] { "BookId", "UserId" }, unique: true, name: "IX_Progression");
        }
        
        public override void Down()
        {
            DropIndex("User.Progression", "IX_Progression");
            AlterColumn("User.Progression", "UserId", c => c.Guid(nullable: false));
            RenameColumn(table: "User.Progression", name: "UserId", newName: "User_Id");
            AddColumn("User.Progression", "UserId", c => c.Guid(nullable: false));
            CreateIndex("User.Progression", "User_Id");
            CreateIndex("User.Progression", new[] { "BookId", "UserId" }, unique: true, name: "IX_Progression");
        }
    }
}
