using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
 
namespace Repositories
{
    public interface IProductRepository
    {
        // IEnumerable<Product> Products { get; }
        // Product this[int id] { get; }
        Product AddProduct(Product product);
        Product UpdateProduct(Product product);
        Product GetProduct(int id);
        IEnumerable<Product> ListProducts();
        void DeleteProduct(int id);
    }
}