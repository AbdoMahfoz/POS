namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MinorChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsAdmin", c => c.Boolean(nullable: false));
            DropColumn("dbo.Items", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Category", c => c.String());
            DropColumn("dbo.Users", "IsAdmin");
        }
    }
}
