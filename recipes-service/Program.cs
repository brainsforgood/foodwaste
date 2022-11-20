using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RecipeService
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static List<Recipe> recipe = new List<Recipe>();
        private static string supercookURL = "https://d1.supercook.com/dyn/results";
        private static string supercookRecipeURL ="https://d1.supercook.com/dyn/details";

        static async Task Main(string[] args)
        {
            
            await CallSuperCook();

        }

        static async Task CallSuperCook()
        {


            // make dynamic 
            Dictionary<string, string> supercookValues = new Dictionary<string, string>{
                {"needsimage","1"},
                {"lang","nl"},
                {"app","1"},
                {"kitchen","tomatenpuree,komkommer,gember"},
                {"start","0"},
                {"cv","2"},
                {"fave","false"}
            };


            try
            {
                // Get top 5 recipes based on ingredients for now
                string supercookResult = await PostHTTPRequestAsyncGetRecipe(supercookURL, supercookValues);
                var dynamicObject = JsonConvert.DeserializeObject<dynamic>(supercookResult)!;

                
                for(int i = 0; i < 5; i++)
                {
                    Recipe rec = new Recipe();
                    rec.title = dynamicObject.results[i].title; 
                    rec.id = dynamicObject.results[i].id;
                    recipe.Add(rec); 
                    //Console.WriteLine(rec.title);
                }

                 for(int i = 0; i < 5; i++)
                {
                    var supercookRecipeValues = new Dictionary<string, string>{
                       {"rid",recipe[i].id},
                       {"lang","nl"}
                    };
                    string supercookRecipe = await PostHTTPRequestAsyncRecipeDetails(supercookRecipeURL, supercookRecipeValues);
                    dynamicObject = JsonConvert.DeserializeObject<dynamic>(supercookRecipe)!;
                    recipe[i].URL = dynamicObject.recipe.hash;
                    //Console.WriteLine(dynamicObject.recipe.hash);
                } 

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task<string> PostHTTPRequestAsyncGetRecipe(string url, Dictionary<string, string> data)
        {
            using (HttpContent formContent = new FormUrlEncodedContent(data))
            {
                using (HttpResponseMessage response = await client.PostAsync(url, formContent).ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
            
            }
        } 

        static async Task<string> PostHTTPRequestAsyncRecipeDetails(string url, Dictionary<string, string> data)
        {
            using (HttpContent formContent = new FormUrlEncodedContent(data))
            {
                using (HttpResponseMessage response = await client.PostAsync(url, formContent).ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
            
            }
        } 
    }

    public class Recipe
    {
        public string title {get; set;}
        public string id {get; set;}
        public string? URL {get; set;}
    }

}
