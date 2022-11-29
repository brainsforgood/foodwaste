using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Products;
 
namespace Repositories
{
    public interface IRepository
    {
        IEnumerable<Product> Products { get; }
        Product this[int id] { get; }
        Product AddProduct(Product product);
        Product UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}