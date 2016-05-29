namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserMakeDateOfBirthNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("User.User", "DateOfBirth", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("User.User", "DateOfBirth", c => c.DateTime(nullable: false));
        }
    }
}
