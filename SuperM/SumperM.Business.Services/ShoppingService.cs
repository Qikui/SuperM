using SuperM.Data.Entities;
using SuperM.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumperM.Business.Services
{
    public class ShoppingService
    {
        private SuperMContext _context;

        public ShoppingService()
        {
            _context = new SuperMContext();
        }

        public int CreateShoppingCart()
        {
            ShoppingCart shoppingCart = new ShoppingCart()
            {
                Created = DateTimeOffset.Now,
            };

            _context.ShoppingCarts.Add(shoppingCart);
            _context.SaveChanges();

            return shoppingCart.ShoppingCartId;
        }

        public void AddItemToShoppingCart(int shoppingCartId, int productId, int qty) 
        {
            ShoppingCart shoppingCart = _context.ShoppingCarts.SingleOrDefault(s => s.ShoppingCartId == shoppingCartId);

            if (shoppingCart == null)
            {
                throw new NullReferenceException();
            }

            ShoppingItemDetails shoppingItemDetails = shoppingCart.ShoppingItems?.SingleOrDefault(s => s.ProductId == productId);

            if (shoppingItemDetails == null)
            {
                shoppingItemDetails = new ShoppingItemDetails()
                {
                    ProductId = productId,
                    Qty = qty
                };
                shoppingCart.ShoppingItems.Add(shoppingItemDetails);
            }
            else
            {
                shoppingItemDetails.Qty += qty;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromShoppingCart(int shoppingCartId, int productId)
        {
            ShoppingCart shoppingCart = _context.ShoppingCarts.SingleOrDefault(s => s.ShoppingCartId == shoppingCartId);

            if (shoppingCart == null)
            {
                throw new NullReferenceException();
            }

            ShoppingItemDetails shoppingItemDetails = shoppingCart.ShoppingItems?.SingleOrDefault(s => s.ProductId == productId);
            if(shoppingItemDetails != null)
            {
                shoppingCart.ShoppingItems.Remove(shoppingItemDetails);
                _context.SaveChanges();
            }
        }

        public int Checkout(int shoppingCartId)
        {
            ShoppingCart shoppingCart = _context.ShoppingCarts.Find(shoppingCartId);
            if(shoppingCart != null && shoppingCart.ShoppingItems != null)
            {
                shoppingCart.CheckoutTime = DateTimeOffset.Now;
                Receipt receipt = new Receipt();
                _context.Receipt.Add(receipt);
                _context.SaveChanges();
                decimal total = 0.0m;
                decimal subTotal = 0.0m;
                foreach (var item in shoppingCart.ShoppingItems)
                {
                    subTotal += item.Qty * item.Product.Price;
                    total += item.Qty * item.Product.Price * 1.15m;

                    Inventory inventory = _context.Inventories.SingleOrDefault(s => s.ProductId == item.ProductId);
                    if (inventory.Qty >= item.Qty)
                        inventory.Qty -= item.Qty;
                    else
                    {
                        //if there is not enough inventory
                    }

                    ReceiptItemDetails receiptItemDetails = new ReceiptItemDetails()
                    {
                        ProductName = item.Product.Name,
                        ReceiptProductId = item.ProductId,
                        Price = item.Product.Price,
                        PurchasedQty = item.Qty,
                        ReceiptId = receipt.ReceiptId,
                        //Receipt = receipt
                    };
                    receipt.ReceiptItems.Add(receiptItemDetails);
                }
                receipt.SubTotal = subTotal;
                receipt.Total = total;
                receipt.TaxRate = 1.15m;
                _context.SaveChanges();
                return receipt.ReceiptId;   //
            }
            else{
                throw new ObjectNotFoundException();
            }
        }

        //call only after a checkout is completed
        public string PrintReceipt(int receiptId)
        {
            Receipt receipt = _context.Receipt.Find(receiptId);
            String message = "This is your receipt";
            return message;
        }
    }
}
