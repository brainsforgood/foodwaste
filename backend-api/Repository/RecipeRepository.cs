using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Repositories
{
    public class RecipeRepository: IRecipeRepository
    {

        private static readonly HttpClient client = new HttpClient();
        private static List<Recipe> recipe = new List<Recipe>();
        private static string supercookURL = "https://d1.supercook.com/dyn/results";
        private static string supercookRecipeURL ="https://d1.supercook.com/dyn/details";

        public RecipeRepository()
        {

        }
 
        public async Task<List<Recipe>> GetRecipe(string product) {
            List<Recipe> r = await CallSuperCook(product);
            System.Console.WriteLine("recipes {0} ", r);
            if (r == null) {
                return null;
            } else {
                return r;
            }
            
        }

        static async Task<List<Recipe>> CallSuperCook(string product)
        {


            // make dynamic 
            Dictionary<string, string> supercookValues = new Dictionary<string, string>{
                {"needsimage","1"},
                {"lang","nl"},
                {"app","1"},
                // {"kitchen","tomatenpuree,komkommer,gember"},
                {"start","0"},
                {"cv","2"},
                {"fave","false"}
            };
            supercookValues.Add("kitchen", product);

            try
            {
                // Get top 5 recipes based on ingredients for now
                string supercookResult = await PostHTTPRequestAsyncGetRecipe(supercookURL, supercookValues);
                var dynamicObject = JsonConvert.DeserializeObject<dynamic>(supercookResult)!;

                
                for(int i = 0; i < 5; i++)
                {
                    Recipe rec = new Recipe();
                    rec.Title = dynamicObject.results[i].title; 
                    rec.ID = dynamicObject.results[i].id;
                    recipe.Add(rec); 
                    //Console.WriteLine(rec.title);
                }

                 for(int i = 0; i < 5; i++)
                {
                    var supercookRecipeValues = new Dictionary<string, string>{
                       {"rid",recipe[i].ID},
                       {"lang","nl"}
                    };
                    string supercookRecipe = await PostHTTPRequestAsyncRecipeDetails(supercookRecipeURL, supercookRecipeValues);
                    dynamicObject = JsonConvert.DeserializeObject<dynamic>(supercookRecipe)!;
                    recipe[i].URL = dynamicObject.recipe.hash;
                    //Console.WriteLine(dynamicObject.recipe.hash);
                    Console.WriteLine(recipe[i].Title);
                } 

                return recipe;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
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
}