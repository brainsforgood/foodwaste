using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;

namespace Controllers;

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