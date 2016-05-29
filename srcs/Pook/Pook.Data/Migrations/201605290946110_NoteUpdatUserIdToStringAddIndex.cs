namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoteUpdatUserIdToStringAddIndex : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "User.Note",
                c => new
                {
                    NoteId = c.Guid(nullable: false),
                    UserId = c.String(maxLength: 128),
                    BookId = c.Guid(nullable: false),
                    Page = c.Int(nullable: false),
                    Description = c.String(),
                    CreatedOn = c.DateTime(),
                    UpdatedOn = c.DateTime(),
                    CreatedBy = c.Guid(),
                    UpdatedBy = c.Guid(),
                    SeoTitle = c.String(),
                })
                .ForeignKey("Book.Book", t => t.BookId, cascadeDelete: true)
                .Index(t => t.UserId)
                .ForeignKey("User.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.BookId);

            AddPrimaryKey("User.Note", "NoteId");
            CreateIndex("User.Note", new[] { "UserId", "BookId", "Page" }, unique: true, name: "IX_Note");
        }
        
        public override void Down()
        {
            DropIndex("User.Note", "IX_Note");
            DropPrimaryKey("User.Note");
            AlterColumn("User.Note", "UserId", c => c.Guid(nullable: false));
            DropColumn("User.Note", "NoteId");
            AddPrimaryKey("User.Note", new[] { "UserId", "BookId" });
            RenameColumn(table: "User.Note", name: "UserId", newName: "User_Id");
            AddColumn("User.Note", "UserId", c => c.Guid(nullable: false));
            CreateIndex("User.Note", "User_Id");
            CreateIndex("User.Note", "BookId");
        }
    }
}
