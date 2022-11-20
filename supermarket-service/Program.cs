using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
    
    class AHProduct {
       
        public int Id  {get;set;}
        public string name {get;set;}
        public string Title  {get;set;  }
        public string ImageUrl  {get;set; }
        public string Brand  {get;set; }
        public IList<ulong> Gtins  {get;set; }
        public int MinBestBeforeDays {get;set; }

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

            foreach (var pType in config.Taxonomy) 
            {
                var url = config.BaseSearchUrl + pType + config.MaxSize;
                var result = await client.GetAsync(url);

                var content = await result.Content.ReadAsStringAsync();
                var dynamicObject = JsonConvert.DeserializeObject<dynamic>(content)!;
            
                var product = new AHProduct();
                System.Console.WriteLine("product group {0} #cards: {1} ", pType, ((JArray)dynamicObject.cards).Count);
                              
                foreach (var c in dynamicObject.cards)
                {
                    product.Id = c.id;
                    if (((JArray)c.products).Count > 0 ) {
                        var p1 = c.products[0];
                        if (p1.title != null) {
                            product.Title = p1.title;
                        }
                        if (((JArray)p1.images).Count > 0 ) {
                            product.ImageUrl = p1.images[0].url;
                        }
                        if (p1.brand != null) {
                            product.Brand =   p1.brand;
                        }
                        if (((JArray)p1.gtins).Count > 0 ) {
                            product.Gtins = p1.gtins.ToObject<List<ulong>>();
                        }
                        if (p1.minBestBeforeDays != null) {
                            product.MinBestBeforeDays = p1.minBestBeforeDays;
                        }

                        product.name = ToName(product.Title,product.Brand);
                    }

                    // string jsonString = JsonConvert.SerializeObject(product);
                    // System.Console.WriteLine("{0} ", jsonString);
                }
            }
        }


    }
}

// https://www.ah.nl/zoeken/api/products/search?taxonomySlug=aardappel-groente-fruit&size=36
// https://www.ah.nl/zoeken/api/products/search?taxonomySlug=zuivel-plantaardig-en-eieren&size= 36
// https://www.ah.nl/zoeken/api/products/search?taxonomySlug=pasta-rijst-en-wereldkeuken&size=36
// https://www.ah.nl/zoeken/api/products/search?taxonomySlug=vlees-kip-vis-vega&size=36
// https://www.ah.nl/zoeken/api/products/search?taxonomySlug=kaas-vleeswaren-tapas&size=36
// https://www.ah.nl/zoeken/api/products/search?taxonomySlug=bakkerij-en-banket&size=36
// https://www.ah.nl/zoeken/api/products/search?taxonomySlug=soepen-sauzen-kruiden-olie&size=36
// https://www.ah.nl/zoeken/api/products/search?taxonomySlug=ontbijtgranen-en-beleg&size=36



// https://www.ah.nl/zoeken/api/products/product?webshopId=54074