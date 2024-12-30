using System;
using System.Collections.Generic;
using System.Linq;
using eshop_auth.Models;
using Microsoft.EntityFrameworkCore;

namespace eshop_auth.Dal
{
    public class ProductsRepository : IProductsRepository, IDisposable
    {
        private readonly ApplicationContext _db;

        public ProductsRepository(ApplicationContext context)
        {
            _db = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _db.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _db.Products.Find(id);
        }

        public void AddProduct(Product product)
        {
            _db.Products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            _db.Entry(product).State = EntityState.Modified;
        }

        public void DeleteProduct(int id)
        {
            var product = _db.Products.Find(id);
            if (product != null)
            {
                _db.Products.Remove(product);
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
