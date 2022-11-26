using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;

namespace SupermarketCSharp
{
    class AHConfiguration {
        public IList<string> Taxonomy = new List<string>{
            "aardappel-groente-fruit",
            "zuivel-plantaardig-en-eieren",
            "pasta-rijst-en-wereldkeuken",
            "vlees-kip-vis-vega",
            "kaas-vleeswaren-tapas",
            "bakkerij-en-banket",
            "soepen-sauzen-kruiden-olie",
            "ontbijtgranen-en-beleg"
        };

        public string BaseSearchUrl = "https://www.ah.nl/zoeken/api/products/search?taxonomySlug=";
        public string MaxSize = "&size=1500";
    }
    class FoodContext : DbContext  {
        public FoodContext() : base()
        {

        }

        public DbSet<AHProduct> Products { get; set; }
        public DbSet<Gtin> Gtins { get; set; }

        string connectionString = "server=localhost;port=3306;database=foodwaste;user=root;password=root";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(connectionString);
        }

    }

    public class AHProduct {
       
        public int ID  {get;set;}
        public int ProductId {get;set;}
        public string Name {get;set;}
        public string Title  {get;set;  }
        public string ImageUrl  {get;set; }
        public string Brand  {get;set; }
        public int MinBestBeforeDays {get;set; }

    }

    class Gtin {
        public int ID {get;set; }
        public ulong Code {get;set; }
        public int ProductId {get;set; }
    }
    
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        private static string ToName(string title, string brand) {
            int pos = title.IndexOf(brand); 
            string withoutBrand = title.Remove(pos, brand.Length);
            return withoutBrand.Trim().ToLower(); 
        }
     
        static async Task Main(string[] args)
        {
            var config = new AHConfiguration();      
            var client = new HttpClient();

            using (FoodContext contextDB = new FoodContext())
            {
                // Create database if not exists
                contextDB.Database.EnsureCreated();                    
                contextDB.Database.OpenConnection();

                List<AHProduct> products = new List<AHProduct>();
 
                foreach (var pType in config.Taxonomy) 
                {
                    var url = config.BaseSearchUrl + pType + config.MaxSize;
                    var result = await client.GetAsync(url);

                    var content = await result.Content.ReadAsStringAsync();
                    var dynamicObject = JsonConvert.DeserializeObject<dynamic>(content)!;
                

                    System.Console.WriteLine("product group {0} #cards: {1} ", pType, ((JArray)dynamicObject.cards).Count);
                    
                
                    foreach (var c in dynamicObject.cards)
                    {
                        var product = new AHProduct();
                        product.ProductId = c.id;
                        if (((JArray)c.products).Count > 0 ) {
                            var p1 = c.products[0];
                            if (p1.title != null) {
                                product.Title = p1.title;
                            }
                            product.ImageUrl = "";
                            if (((JArray)p1.images).Count > 0 ) {
                                product.ImageUrl = p1.images[0].url;
                            }
                            if (p1.brand != null) {
                                product.Brand =   p1.brand;
                            }
                            if (((JArray)p1.gtins).Count > 0 ) {
                                List<Gtin> gtins = new List<Gtin>();
                                foreach (var g in p1.gtins) {
                                    var gtin = new Gtin();
                                    gtin.Code = g;
                                    gtin.ProductId = product.ProductId;
                                    gtins.Add(gtin);
                                }
                                contextDB.Gtins.AddRange(gtins);
                            }
                            product.MinBestBeforeDays = 0;
                            if (p1.minBestBeforeDays != null) {
                                product.MinBestBeforeDays = p1.minBestBeforeDays;
                            }

                            product.Name = ToName(product.Title,product.Brand);
                            products.Add(product);
                        }
                    }
                }
                contextDB.Products.AddRange(products);
                contextDB.SaveChanges();
                contextDB.Database.CloseConnection();
            }
        }
    }
}
