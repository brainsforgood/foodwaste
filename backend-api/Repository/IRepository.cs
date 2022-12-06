using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
 
namespace Repositories
{
    public interface IProductRepository
    {
        Product AddProduct(Product product);
        Product UpdateProduct(Product product);
        Product GetProduct(int id);
        Product GetProductByGtin(ulong code);
        IEnumerable<Product> ListProducts();
        void DeleteProduct(int id);
    }
}