using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Products;
 
namespace Repositories
{
    public class Repository: IRepository
    {
        private Dictionary<int, Product> items;
         
        public Repository()
        {
            items = new Dictionary<int, Product>();
            new List<Product> {
                new Product {ID=1, Name = "komkommer", Brand = "AH", ProductId=1234 },
                new Product {ID=2, Name = "volkorenbrood", Brand = "AH", ProductId=5678 },
                new Product {ID=3, Name = "melk", Brand = "Optimel", ProductId=9012 }
                }.ForEach(r => AddProduct(r));
        }
        public Product this[int id] => items.ContainsKey(id) ? items[id] : null;
         
        public IEnumerable<Product> Products => items.Values;
         
        public Product AddProduct(Product product)
        {
            if (product.ID == 0)
            {
                int key = items.Count;
                while (items.ContainsKey(key)) { key++; };
                product.ID = key;
            }
            items[product.ID] = product;
            return product;
        }
         
        public void DeleteProduct(int id) => items.Remove(id);
         
        public Product UpdateProduct(Product product) => AddProduct(product);
    }
}