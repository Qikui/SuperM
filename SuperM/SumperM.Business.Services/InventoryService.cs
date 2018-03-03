using SuperM.Data.Entities;
using SuperM.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumperM.Business.Services
{
    public class InventoryService
    {
        private SuperMContext _context;
        

        public InventoryService()
        {
            _context = new SuperMContext();
        }

        public List<Inventory> AllInventories()
        {
            return _context.Inventories.ToList();
        }

        public void Edit(Inventory inventory)
        {
            Inventory inventoryToChange = _context.Inventories.Find(inventory.InventoryId);

            inventoryToChange.Qty = inventory.Qty;
            inventoryToChange.Description = inventory.Description ?? inventoryToChange.Description;
            //inventoryToChange.ProductId = inventory.ProductId ?? inventoryToChange.ProductId;
            //inventoryToChange.Product = inventory.Product ?? inventoryToChange.Product;

            _context.SaveChanges();
        }

        public void Delete(Inventory inventory)
        {
            _context.Inventories.Remove(inventory);
            _context.SaveChanges();
        }

        public void Add(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            _context.SaveChanges();
        }


        //return qty or null
        public int GetQuantity(int inventoryId)
        {
            return _context.Inventories.Find(inventoryId).Qty;
        }


        public string GetProductDescription(int inventoryId)
        {
            return _context.Inventories.Find(inventoryId).Product.Description;
        }

        public Product GetProduct(int inventoryId)
        {
            return (Product)_context.Inventories.Find(inventoryId).Product;
        }

        public List<Inventory> QtyLessThanThreshold(int threshold=5)
        {
            List<Inventory> inventoryList = _context.Inventories.SkipWhile(s => s.Qty >= threshold).ToList();
            return inventoryList;
        }

        public Inventory GetInventory(Product product)
        {
            return _context.Inventories.Find(product.ProductId);
        }
    }
}
