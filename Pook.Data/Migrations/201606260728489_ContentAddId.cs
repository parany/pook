namespace Pook.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ContentAddId : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("Book.Responsability", "AuthorId", "Book.Author");
            //DropForeignKey("User.Note", "BookId", "Book.Book");
            //DropForeignKey("User.Progression", "BookId", "Book.Book");
            //DropForeignKey("Book.Responsability", "BookId", "Book.Book");
            //DropForeignKey("Book.Book", "CategoryId", "Book.Category");
            //DropForeignKey("Book.Book", "EditorId", "Editor.Editor");
            //DropForeignKey("Book.Book", "FirmId", "Editor.Firm");
            //DropForeignKey("User.Progression", "StatusId", "User.Status");
            //DropForeignKey("Book.Responsability", "ResponsabilityTypeId", "Book.ResponsabilityType");

            //DropPrimaryKey("Book.Author");
            //DropPrimaryKey("Book.Book");
            //DropPrimaryKey("Book.Category");
            //DropPrimaryKey("Editor.Editor");
            //DropPrimaryKey("Editor.Firm");
            //DropPrimaryKey("User.Note");
            //DropPrimaryKey("User.Progression");
            //DropPrimaryKey("User.Status");
            //DropPrimaryKey("Book.Responsability");
            //DropPrimaryKey("Book.ResponsabilityType");

            //AddColumn("Book.Author", "Id", c => c.Guid(nullable: false));
            AddColumn("Book.Author", "CreatedOn", c => c.DateTime());
            AddColumn("Book.Author", "UpdatedOn", c => c.DateTime());
            AddColumn("Book.Author", "CreatedBy", c => c.Guid());
            AddColumn("Book.Author", "UpdatedBy", c => c.Guid());
            AddColumn("Book.Author", "SeoTitle", c => c.String());

            //AddColumn("Book.Book", "Id", c => c.Guid(nullable: false));
            //AddColumn("Book.Category", "Id", c => c.Guid(nullable: false));
            //AddColumn("Editor.Editor", "Id", c => c.Guid(nullable: false));
            //AddColumn("Editor.Firm", "Id", c => c.Guid(nullable: false));
            //AddColumn("User.Note", "Id", c => c.Guid(nullable: false));
            //AddColumn("User.Progression", "Id", c => c.Guid(nullable: false));
            //AddColumn("User.Status", "Id", c => c.Guid(nullable: false));
            //AddColumn("Book.Responsability", "Id", c => c.Guid(nullable: false));

            AddColumn("Book.Responsability", "CreatedOn", c => c.DateTime());
            AddColumn("Book.Responsability", "UpdatedOn", c => c.DateTime());
            AddColumn("Book.Responsability", "CreatedBy", c => c.Guid());
            AddColumn("Book.Responsability", "UpdatedBy", c => c.Guid());
            AddColumn("Book.Responsability", "SeoTitle", c => c.String());
            //AddColumn("Book.ResponsabilityType", "Id", c => c.Guid(nullable: false));
            AddColumn("Book.ResponsabilityType", "CreatedOn", c => c.DateTime());
            AddColumn("Book.ResponsabilityType", "UpdatedOn", c => c.DateTime());
            AddColumn("Book.ResponsabilityType", "CreatedBy", c => c.Guid());
            AddColumn("Book.ResponsabilityType", "UpdatedBy", c => c.Guid());
            AddColumn("Book.ResponsabilityType", "SeoTitle", c => c.String());

            //AddPrimaryKey("Book.Author", "Id");
            //AddPrimaryKey("Book.Book", "Id");
            //AddPrimaryKey("Book.Category", "Id");
            //AddPrimaryKey("Editor.Editor", "Id");
            //AddPrimaryKey("Editor.Firm", "Id");
            //AddPrimaryKey("User.Note", "Id");
            //AddPrimaryKey("User.Progression", "Id");
            //AddPrimaryKey("User.Status", "Id");
            //AddPrimaryKey("Book.Responsability", "Id");
            //AddPrimaryKey("Book.ResponsabilityType", "Id");

            //AddForeignKey("Book.Responsability", "AuthorId", "Book.Author", "Id", cascadeDelete: true);
            //AddForeignKey("User.Note", "BookId", "Book.Book", "Id", cascadeDelete: true);
            //AddForeignKey("User.Progression", "BookId", "Book.Book", "Id", cascadeDelete: true);
            //AddForeignKey("Book.Responsability", "BookId", "Book.Book", "Id", cascadeDelete: true);
            //AddForeignKey("Book.Book", "CategoryId", "Book.Category", "Id", cascadeDelete: true);
            //AddForeignKey("Book.Book", "EditorId", "Editor.Editor", "Id");
            //AddForeignKey("Book.Book", "FirmId", "Editor.Firm", "Id");
            //AddForeignKey("User.Progression", "StatusId", "User.Status", "Id", cascadeDelete: true);
            //AddForeignKey("Book.Responsability", "ResponsabilityTypeId", "Book.ResponsabilityType", "Id", cascadeDelete: true);

            //DropColumn("Book.Author", "AuthorId");
            //DropColumn("Book.Book", "BookId");
            //DropColumn("Book.Category", "CategoryId");
            //DropColumn("Editor.Editor", "EditorId");
            //DropColumn("Editor.Firm", "FirmId");
            //DropColumn("User.Note", "NoteId");
            //DropColumn("User.Progression", "ProgressionId");
            //DropColumn("User.Status", "StatusId");
            //DropColumn("Book.Responsability", "ResponsabilityId");
            //DropColumn("Book.ResponsabilityType", "ResponsabilityTypeId");
        }
        
        public override void Down()
        {
            AddColumn("Book.ResponsabilityType", "ResponsabilityTypeId", c => c.Guid(nullable: false));
            AddColumn("Book.Responsability", "ResponsabilityId", c => c.Guid(nullable: false));
            AddColumn("User.Status", "StatusId", c => c.Guid(nullable: false));
            AddColumn("User.Progression", "ProgressionId", c => c.Guid(nullable: false));
            AddColumn("User.Note", "NoteId", c => c.Guid(nullable: false));
            AddColumn("Editor.Firm", "FirmId", c => c.Guid(nullable: false));
            AddColumn("Editor.Editor", "EditorId", c => c.Guid(nullable: false));
            AddColumn("Book.Category", "CategoryId", c => c.Guid(nullable: false));
            AddColumn("Book.Book", "BookId", c => c.Guid(nullable: false));
            AddColumn("Book.Author", "AuthorId", c => c.Guid(nullable: false));
            DropForeignKey("Book.Responsability", "ResponsabilityTypeId", "Book.ResponsabilityType");
            DropForeignKey("User.Progression", "StatusId", "User.Status");
            DropForeignKey("Book.Book", "FirmId", "Editor.Firm");
            DropForeignKey("Book.Book", "EditorId", "Editor.Editor");
            DropForeignKey("Book.Book", "CategoryId", "Book.Category");
            DropForeignKey("Book.Responsability", "BookId", "Book.Book");
            DropForeignKey("User.Progression", "BookId", "Book.Book");
            DropForeignKey("User.Note", "BookId", "Book.Book");
            DropForeignKey("Book.Responsability", "AuthorId", "Book.Author");
            DropPrimaryKey("Book.ResponsabilityType");
            DropPrimaryKey("Book.Responsability");
            DropPrimaryKey("User.Status");
            DropPrimaryKey("User.Progression");
            DropPrimaryKey("User.Note");
            DropPrimaryKey("Editor.Firm");
            DropPrimaryKey("Editor.Editor");
            DropPrimaryKey("Book.Category");
            DropPrimaryKey("Book.Book");
            DropPrimaryKey("Book.Author");
            DropColumn("Book.ResponsabilityType", "SeoTitle");
            DropColumn("Book.ResponsabilityType", "UpdatedBy");
            DropColumn("Book.ResponsabilityType", "CreatedBy");
            DropColumn("Book.ResponsabilityType", "UpdatedOn");
            DropColumn("Book.ResponsabilityType", "CreatedOn");
            DropColumn("Book.ResponsabilityType", "Id");
            DropColumn("Book.Responsability", "SeoTitle");
            DropColumn("Book.Responsability", "UpdatedBy");
            DropColumn("Book.Responsability", "CreatedBy");
            DropColumn("Book.Responsability", "UpdatedOn");
            DropColumn("Book.Responsability", "CreatedOn");
            DropColumn("Book.Responsability", "Id");
            DropColumn("User.Status", "Id");
            DropColumn("User.Progression", "Id");
            DropColumn("User.Note", "Id");
            DropColumn("Editor.Firm", "Id");
            DropColumn("Editor.Editor", "Id");
            DropColumn("Book.Category", "Id");
            DropColumn("Book.Book", "Id");
            DropColumn("Book.Author", "SeoTitle");
            DropColumn("Book.Author", "UpdatedBy");
            DropColumn("Book.Author", "CreatedBy");
            DropColumn("Book.Author", "UpdatedOn");
            DropColumn("Book.Author", "CreatedOn");
            DropColumn("Book.Author", "Id");
            AddPrimaryKey("Book.ResponsabilityType", "ResponsabilityTypeId");
            AddPrimaryKey("Book.Responsability", "ResponsabilityId");
            AddPrimaryKey("User.Status", "StatusId");
            AddPrimaryKey("User.Progression", "ProgressionId");
            AddPrimaryKey("User.Note", "NoteId");
            AddPrimaryKey("Editor.Firm", "FirmId");
            AddPrimaryKey("Editor.Editor", "EditorId");
            AddPrimaryKey("Book.Category", "CategoryId");
            AddPrimaryKey("Book.Book", "BookId");
            AddPrimaryKey("Book.Author", "AuthorId");
            AddForeignKey("Book.Responsability", "ResponsabilityTypeId", "Book.ResponsabilityType", "ResponsabilityTypeId", cascadeDelete: true);
            AddForeignKey("User.Progression", "StatusId", "User.Status", "StatusId", cascadeDelete: true);
            AddForeignKey("Book.Book", "FirmId", "Editor.Firm", "FirmId");
            AddForeignKey("Book.Book", "EditorId", "Editor.Editor", "EditorId");
            AddForeignKey("Book.Book", "CategoryId", "Book.Category", "CategoryId", cascadeDelete: true);
            AddForeignKey("Book.Responsability", "BookId", "Book.Book", "BookId", cascadeDelete: true);
            AddForeignKey("User.Progression", "BookId", "Book.Book", "BookId", cascadeDelete: true);
            AddForeignKey("User.Note", "BookId", "Book.Book", "BookId", cascadeDelete: true);
            AddForeignKey("Book.Responsability", "AuthorId", "Book.Author", "AuthorId", cascadeDelete: true);
        }
    }
}
