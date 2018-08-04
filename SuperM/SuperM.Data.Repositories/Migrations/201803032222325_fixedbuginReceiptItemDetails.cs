namespace SuperM.Data.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedbuginReceiptItemDetails : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReceiptItemDetails", "ShoppingCartId", "dbo.ShoppingCarts");
            DropForeignKey("dbo.ReceiptItemDetails", "Receipt_ReceiptId", "dbo.Receipts");
            DropIndex("dbo.ReceiptItemDetails", new[] { "ShoppingCartId" });
            DropIndex("dbo.ReceiptItemDetails", new[] { "Receipt_ReceiptId" });
            RenameColumn(table: "dbo.ReceiptItemDetails", name: "Receipt_ReceiptId", newName: "ReceiptId");
            AddColumn("dbo.ShoppingItemDetails", "Qty", c => c.Int(nullable: false));
            AlterColumn("dbo.ReceiptItemDetails", "ReceiptId", c => c.Int(nullable: false));
            CreateIndex("dbo.ReceiptItemDetails", "ReceiptId");
            AddForeignKey("dbo.ReceiptItemDetails", "ReceiptId", "dbo.Receipts", "ReceiptId", cascadeDelete: true);
            DropColumn("dbo.ReceiptItemDetails", "ShoppingCartId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReceiptItemDetails", "ShoppingCartId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ReceiptItemDetails", "ReceiptId", "dbo.Receipts");
            DropIndex("dbo.ReceiptItemDetails", new[] { "ReceiptId" });
            AlterColumn("dbo.ReceiptItemDetails", "ReceiptId", c => c.Int());
            DropColumn("dbo.ShoppingItemDetails", "Qty");
            RenameColumn(table: "dbo.ReceiptItemDetails", name: "ReceiptId", newName: "Receipt_ReceiptId");
            CreateIndex("dbo.ReceiptItemDetails", "Receipt_ReceiptId");
            CreateIndex("dbo.ReceiptItemDetails", "ShoppingCartId");
            AddForeignKey("dbo.ReceiptItemDetails", "Receipt_ReceiptId", "dbo.Receipts", "ReceiptId");
            AddForeignKey("dbo.ReceiptItemDetails", "ShoppingCartId", "dbo.ShoppingCarts", "ShoppingCartId", cascadeDelete: true);
        }
    }
}
