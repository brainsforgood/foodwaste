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
        public DbSet<Gtin> Gtins { get; set; }
  

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

        }
 
        public Product GetProduct(int id) {
            using (ProductContext contextDB = new ProductContext())
            {                  
                var product = contextDB.Products.Where(p => p.ID == id).First();
                return product;
            }
        }

         public Product GetProductByGtin(ulong code) {
            using (ProductContext contextDB = new ProductContext())
            {       
                System.Console.WriteLine("code: {0} ", code);         
                var gtin = contextDB.Gtins.Where(g => g.Code == code).First();
                var product = contextDB.Products.Where(p => p.ProductId == gtin.ProductId).First();
                return product;
            }
        }

        public IEnumerable<Product> ListProducts() {
            using (ProductContext contextDB = new ProductContext())
            {                  
                List<Product> products = contextDB.Products.Where(p => p.ID > 0).ToList();   
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