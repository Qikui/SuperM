namespace SuperM.Data.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateInventoryEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        InventoryId = c.Int(nullable: false, identity: true),
                        Qty = c.Int(nullable: false),
                        Description = c.String(),
                        ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.InventoryId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inventories", "ProductId", "dbo.Products");
            DropIndex("dbo.Inventories", new[] { "ProductId" });
            DropTable("dbo.Inventories");
        }
    }
}
