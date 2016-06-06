namespace Pook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookAddNumberOfPages : DbMigration
    {
        public override void Up()
        {
            AddColumn("Book.Book", "NumberOfPages", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Book.Book", "NumberOfPages");
        }
    }
}
