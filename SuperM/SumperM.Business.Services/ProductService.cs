using SuperM.Data.Entities;
using SuperM.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumperM.Business.Services
{
    public class ProductService
    {
        private SuperMContext _context;

        public ProductService()
        {
            this._context = new SuperMContext();
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public void Edit(Product product)
        {
            Product productToChange = _context.Products.FirstOrDefault(s => s.ProductId == product.ProductId);

            productToChange.Name = product.Name;
            productToChange.Price = product.Price;
            productToChange.Category = product.Category;
            productToChange.Description = product.Description;

            _context.SaveChanges();
            
        }

        public Product GetById(int productId)
        {
            Product product = _context.Products.Find(productId);
            return product;
        }

        public Product GetByName(string name)
        {
            Product product = _context.Products.FirstOrDefault(s => s.Name == name.Trim());
            return product;
        }

        public List<Product> AllProducts()
        {
            return _context.Products.ToList();
        }
    }
}
