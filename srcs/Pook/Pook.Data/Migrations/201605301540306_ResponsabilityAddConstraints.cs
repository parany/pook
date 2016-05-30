namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResponsabilityAddConstraints : DbMigration
    {
        public override void Up()
        {
            DropIndex("Book.Responsability", new[] { "AuthorId" });
            DropIndex("Book.Responsability", new[] { "BookId" });
            CreateIndex("Book.Responsability", new[] { "AuthorId", "BookId" }, unique: true, name: "IX_Responsability");
        }
        
        public override void Down()
        {
            DropIndex("Book.Responsability", "IX_Responsability");
            CreateIndex("Book.Responsability", "BookId");
            CreateIndex("Book.Responsability", "AuthorId");
        }
    }
}
