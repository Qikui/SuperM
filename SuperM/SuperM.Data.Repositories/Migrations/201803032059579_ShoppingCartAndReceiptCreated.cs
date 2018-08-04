namespace SuperM.Data.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShoppingCartAndReceiptCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Receipts",
                c => new
                    {
                        ReceiptId = c.Int(nullable: false, identity: true),
                        SubTotal = c.Int(nullable: false),
                        TaxRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReceiptId);
            
            CreateTable(
                "dbo.ReceiptItemDetails",
                c => new
                    {
                        ReceiptItemDetailsId = c.Int(nullable: false, identity: true),
                        ReceiptProductId = c.Int(nullable: false),
                        ProductName = c.String(),
                        PurchasedQty = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShoppingCartId = c.Int(nullable: false),
                        Receipt_ReceiptId = c.Int(),
                    })
                .PrimaryKey(t => t.ReceiptItemDetailsId)
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCartId, cascadeDelete: true)
                .ForeignKey("dbo.Receipts", t => t.Receipt_ReceiptId)
                .Index(t => t.ShoppingCartId)
                .Index(t => t.Receipt_ReceiptId);
            
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        ShoppingCartId = c.Int(nullable: false, identity: true),
                        CheckoutTime = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.ShoppingCartId);
            
            CreateTable(
                "dbo.ShoppingItemDetails",
                c => new
                    {
                        ShoppingItemDetailsID = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ShoppingCartId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShoppingItemDetailsID)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCartId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ShoppingCartId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReceiptItemDetails", "Receipt_ReceiptId", "dbo.Receipts");
            DropForeignKey("dbo.ReceiptItemDetails", "ShoppingCartId", "dbo.ShoppingCarts");
            DropForeignKey("dbo.ShoppingItemDetails", "ShoppingCartId", "dbo.ShoppingCarts");
            DropForeignKey("dbo.ShoppingItemDetails", "ProductId", "dbo.Products");
            DropIndex("dbo.ShoppingItemDetails", new[] { "ShoppingCartId" });
            DropIndex("dbo.ShoppingItemDetails", new[] { "ProductId" });
            DropIndex("dbo.ReceiptItemDetails", new[] { "Receipt_ReceiptId" });
            DropIndex("dbo.ReceiptItemDetails", new[] { "ShoppingCartId" });
            DropTable("dbo.ShoppingItemDetails");
            DropTable("dbo.ShoppingCarts");
            DropTable("dbo.ReceiptItemDetails");
            DropTable("dbo.Receipts");
        }
    }
}
