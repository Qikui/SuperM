using SuperM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperM.Data.Repositories
{
    public class SuperMContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<ShoppingItemDetails> ShoppingItemDetails { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ReceiptItemDetails> ReceiptItemDetails { get; set; }
        public DbSet<Receipt> Receipt { get; set; }
        
        public SuperMContext() : base() { }

        public SuperMContext(string connectingString) : base(connectingString) { }
        
    }
}
