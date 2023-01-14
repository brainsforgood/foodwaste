using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;

namespace Controllers;

[ApiKeyAuth("kW4BE3kvMb")]
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private IProductRepository repository;
    public ProductController(IProductRepository repo) => repository = repo;

    [HttpGet()]
    public IEnumerable<Product> Get() 
    {
        return repository.ListProducts();
    }
 
    [HttpGet("{id}")]
    public ActionResult<Product> Get(int id)
    {
        return Ok(repository.GetProduct(id));
    }

    [HttpGet("gtin/{code}")]
    public ActionResult<Product> GetGtin(ulong code)
    {
        return Ok(repository.GetProductByGtin(code));
    }
    

}
// public class ApiKeyAuthAttribute : ActionFilterAttribute
// {
//     private const string ApiKeyHeaderName = "ApiKey";
//     private readonly string _apiKey;

//     public ApiKeyAuthAttribute(string apiKey)
//     {
//         _apiKey = apiKey;
//     }

//     public override void OnActionExecuting(ActionExecutingContext context)
//     {
//         if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyHeaderValues))
//         {
//             context.Result = new UnauthorizedResult();
//             return;
//         }

//         var providedApiKey = apiKeyHeaderValues.FirstOrDefault();

//         if (providedApiKey != _apiKey)
//         {
//             context.Result = new UnauthorizedResult();
//             return;
//         }

//         base.OnActionExecuting(context);
//     }
// }
