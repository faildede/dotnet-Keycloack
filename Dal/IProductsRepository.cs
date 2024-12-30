using System.Collections.Generic;
using eshop_auth.Models;

namespace eshop_auth.Dal
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProductById(int id);
        void AddProduct(Product products);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}
