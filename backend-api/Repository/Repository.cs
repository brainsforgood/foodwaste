using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
 
namespace Repositories
{
    class ProductContext : DbContext  {
        public ProductContext() : base()
        {

        }

        public DbSet<Product> Products { get; set; }
  

        string connectionString = "server=localhost;port=3306;database=foodwaste;user=root;password=root";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(connectionString);
        }

    }

    public class ProductRepository: IProductRepository
    {

        private Dictionary<int, Product> items;
         
        public ProductRepository()
        {
            items = new Dictionary<int, Product>();
            new List<Product> {
                new Product {ID=1, Name = "komkommer", Brand = "AH", ProductId=1234 },
                new Product {ID=2, Name = "volkorenbrood", Brand = "AH", ProductId=5678 },
                new Product {ID=3, Name = "melk", Brand = "Optimel", ProductId=9012 }
                }.ForEach(r => AddProduct(r));
        }
 
        public Product GetProduct(int id) {
            using (ProductContext contextDB = new ProductContext())
            {                  
                contextDB.Database.OpenConnection();
                var product = contextDB.Products.Where(p => p.ID == id).First();
                contextDB.Database.CloseConnection();
                return product;
            }
        }

        public IEnumerable<Product> ListProducts() {
            using (ProductContext contextDB = new ProductContext())
            {                  
                contextDB.Database.OpenConnection();
                var products = contextDB.Products.Where(p => p.ID > 0);
                contextDB.Database.CloseConnection();
                return products;
            }
        }
                  
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