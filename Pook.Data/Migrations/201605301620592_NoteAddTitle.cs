namespace Pook.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class NoteAddTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("User.Note", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("User.Note", "Title");
        }
    }
}
