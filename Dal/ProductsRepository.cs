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
            var product = _db.Products.Find(id);
            if (product == null){
             return null;
            }
            return product;
        }

        public Product GetProductByName(string title)
        {
            var product = _db.Products.FirstOrDefault(p => p.Title == title);
            return product;
        }
        
        public void AddProduct(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _db.Entry(product).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _db.Products.Find(id);
            if (product != null)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
            }
            else
            {
                return ;
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
