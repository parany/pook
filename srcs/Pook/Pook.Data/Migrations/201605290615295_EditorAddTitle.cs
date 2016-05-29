namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditorAddTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("Editor.Editor", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Editor.Editor", "Title");
        }
    }
}
