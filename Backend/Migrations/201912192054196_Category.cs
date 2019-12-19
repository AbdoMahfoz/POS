namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AddedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemCategories", "ItemId", "dbo.Items");
            DropForeignKey("dbo.ItemCategories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.ItemCategories", new[] { "CategoryId" });
            DropIndex("dbo.ItemCategories", new[] { "ItemId" });
            DropTable("dbo.ItemCategories");
            DropTable("dbo.Categories");
        }
    }
}
