using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;

namespace Controllers;

[ApiKeyAuth("kW4BE3kvMb")]
[ApiController]
[Route("api/[controller]")]
public class RecipeController : ControllerBase
{
    private IRecipeRepository repository;
    public RecipeController(IRecipeRepository repo) => repository = repo;

  
    [HttpGet()]
    public async Task<ActionResult<Recipe>> Get([FromQuery]string product)
    {
        System.Console.WriteLine("prod: {0} ", product); 
        var prod = await repository.GetRecipe(product);
  
        return Ok(prod);
    }

    

}